using OFA.Common.Messages.Events;
using OFA.DAL.EventStore.DAL.IDAL;
using OFA.Repayment.WM.Messages.Events;
using OFA.Repayment.WM.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OFA.Repayment.WM.Repositories
{
    public class SeasonRepository : ISeasonRepository
    {
        private readonly IOFAEventStore _eventStore;
        public SeasonRepository(IOFAEventStore eventStore)
        {
            _eventStore = eventStore;
        }
        public async Task<IEnumerable<IEvent>> GetAllAsync(string id)
            => await _eventStore.ReadAllEventsASync<SeasonCreated>(id);

        public async Task SaveAsync(string streamName, IEvent @event)
            => await _eventStore.AppendEventAsync(streamName, @event);
    }
}
