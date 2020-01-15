using OFA.Accounts.WM.Messages.Events;
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
        public Task GetActiveAccountAsync(string accountName)
        {
            throw new NotImplementedException();
        }
    }
}
