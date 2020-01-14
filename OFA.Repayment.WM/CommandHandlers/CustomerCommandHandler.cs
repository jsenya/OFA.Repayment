using OFA.Repayment.WM.CommandHandlers.ICommandHandlers;
using OFA.Repayment.WM.Messages.Events;
using OFA.Repayment.WM.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OFA.Repayment.WM.CommandHandlers
{
    public class CustomerCommandHandler : ICustomerCommandHandler<CustomerCreated>
    {
        private readonly ICustomerRepository _repository;
        public CustomerCommandHandler(ICustomerRepository repository)
        {
            _repository = repository;
        }

        public Task AppendAsync(CustomerCreated @event)
        {
            throw new NotImplementedException();
        }
    }
}
