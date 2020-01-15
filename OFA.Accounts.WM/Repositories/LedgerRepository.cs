using Newtonsoft.Json;
using OFA.Accounts.WM.Messages.Events;
using OFA.Accounts.WM.Projections;
using OFA.Accounts.WM.Repositories.IRepositories;
using OFA.Common.Messages.Events;
using OFA.DAL.EventStore.DAL.IDAL;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OFA.Accounts.WM.Repositories
{
    public class LedgerRepository : ILedgerRepository
    {
        private readonly IOFAEventStore _eventStore;
        public LedgerRepository(IOFAEventStore eventStore)
        {
            _eventStore = eventStore;
        }
        public async Task<IEnumerable<IEvent>> GetAllAsync(string id)
            => await _eventStore.ReadAllEventsASync<LedgerDebitEntryCreated>(id);

        public async Task SaveAsync(string streamName, IEvent @event)
            => await _eventStore.AppendEventAsync(streamName, @event);
        public async Task<GlEntry> GetPendingEntriesAsync(string projectionName)
        {
            try
            {
                var result = await _eventStore.GetProjectionResultAsync(projectionName);

                if (!string.IsNullOrEmpty(result))
                    return JsonConvert.DeserializeObject<GlEntry>(result);

                return null;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task CreateProjectionAsync(string projectionName, string query)
            => await _eventStore.CreateProjectionAsync(projectionName, query);
    }
}
