using OFA.Repayment.WM.CommandHandlers.ICommandHandlers;
using OFA.Repayment.WM.Messages.Commands;
using OFA.Repayment.WM.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OFA.Repayment.WM.CommandHandlers
{
    public class CustomerSummaryCommandHandler : ICustomerSummaryCommandHandler<CreateCustomerSummary>
    {
        private readonly ICustomerSummaryRepository _repository;
        public CustomerSummaryCommandHandler(ICustomerSummaryRepository repository)
        {
            _repository = repository;
        }
        public async Task HandleAsync(CreateCustomerSummary command)
        {
            try
            {
                await _repository.SaveAsync("customer-summary", command.@event);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
