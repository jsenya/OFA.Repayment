using OFA.DAL.EventStore.DAL.IDAL;
using OFA.Repayment.WM.Repositories.IRepositories;
using System;
using System.Threading.Tasks;

namespace OFA.Repayment.WM.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly IOFAEventStore _eventStore;
        public CustomerRepository(IOFAEventStore eventStore)
        {
            _eventStore = eventStore;
        }
        public Task GetByIdAsync<TId>(TId id)
        {
            throw new NotImplementedException();
        }

        public Task SaveAsync()
        {
            throw new NotImplementedException();
        }
    }
}
