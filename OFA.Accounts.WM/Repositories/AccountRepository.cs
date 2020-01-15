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
    public class AccountRepository : IAccountRepository
    {
        private readonly IOFAEventStore _eventStore;
        public AccountRepository(IOFAEventStore eventStore)
        {
            _eventStore = eventStore;
        }
        public async Task<IEnumerable<IEvent>> GetAllAsync(string id)
            => await _eventStore.ReadAllEventsASync<AccountCreated>(id);

        public async Task SaveAsync(string streamName, IEvent @event)
            => await _eventStore.AppendEventAsync(streamName, @event);
    }
}
