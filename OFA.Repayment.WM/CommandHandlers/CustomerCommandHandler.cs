using OFA.Repayment.WM.CommandHandlers.ICommandHandlers;
using OFA.Repayment.WM.Messages.Commands;
using OFA.Repayment.WM.Messages.Events;
using OFA.Repayment.WM.Repositories.IRepositories;
using System;
using System.Threading.Tasks;

namespace OFA.Repayment.WM.CommandHandlers
{
    public class CustomerCommandHandler : ICustomerCommandHandler<CreateCustomer>
    {
        private readonly ICustomerRepository _repository;
        public CustomerCommandHandler(ICustomerRepository repository)
        {
            _repository = repository;
        }

        public async Task HandleAsync(CreateCustomer command)
        {
            try
            {
                //since it's a 3rd party event we don't need to do any business logic just save the event
                await _repository.SaveAsync("customers", command.@event);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
