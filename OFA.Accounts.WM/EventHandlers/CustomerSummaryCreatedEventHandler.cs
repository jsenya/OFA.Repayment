using OFA.Accounts.WM.CommandHandlers.ICommandHandlers;
using OFA.Accounts.WM.EventHandlers.IEventHandlers;
using OFA.Accounts.WM.Messages.Commands;
using OFA.Accounts.WM.Messages.Events;
using OFA.Common.Messages.Commands;
using System;
using System.Threading.Tasks;

namespace OFA.Accounts.WM.EventHandlers
{
    public class CustomerSummaryCreatedEventHandler : ICustomerSummaryCreatedEventHandler<CustomerSummaryCreated>
    {
        private readonly IAccountCreatedCommandHandler<CreateAccount> _accountCreatedCH;
        public CustomerSummaryCreatedEventHandler(IAccountCreatedCommandHandler<CreateAccount> accCreatedCH)
        {
            _accountCreatedCH = accCreatedCH;
        }
        public async Task HandlerAsync(CustomerSummaryCreated @event)
        {
            try
            {
                //create a corresponding command to create a loan account for the customer summary that has just come in
                CreateAccount command = new CreateAccount(@event.CustomerId, @event.SeasonId);
                await _accountCreatedCH.HandleAsync(command);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
