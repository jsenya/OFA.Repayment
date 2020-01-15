using OFA.Accounts.WM.CommandHandlers.ICommandHandlers;
using OFA.Accounts.WM.EventHandlers.IEventHandlers;
using OFA.Accounts.WM.Messages.Commands;
using OFA.Accounts.WM.Messages.Events;
using OFA.Common.Messages.Commands;
using System;
using System.Threading.Tasks;
using static OFA.Accounts.WM.Helpers;

namespace OFA.Accounts.WM.EventHandlers
{
    public class CustomerSummaryCreatedEventHandler : ICustomerSummaryCreatedEventHandler<CustomerSummaryCreated>
    {
        //private readonly ICreateAccountCommandHandler<CreateAccount> _accountCreatedCH;
        private readonly ICreateInitialLedgerEntryCommandHandler<CreateInitialLedgerEntry> _createLedgerDebitEntryCH;
        public CustomerSummaryCreatedEventHandler(ICreateInitialLedgerEntryCommandHandler<CreateInitialLedgerEntry> createLedgerDebitEntryCH)
        {
            //_accountCreatedCH = accCreatedCH;
            _createLedgerDebitEntryCH = createLedgerDebitEntryCH;
        }
        public async Task HandlerAsync(CustomerSummaryCreated @event)
        {
            try
            {
                ////create a corresponding command to create a loan account for the customer summary that has just come in
                //CreateAccount command = new CreateAccount(@event.CustomerId, @event.SeasonId, @event.EventId);
                //await _accountCreatedCH.HandleAsync(command);
                int runningBal = 0;
                //create 2 intial ledger entries
                //1.) opening balance
                var ob = new CreateInitialLedgerEntry(@event.CustomerId, 0, 0, runningBal, "Opening Balance", @event.EventId, @event.SeasonId);
                await _createLedgerDebitEntryCH.HandleAsync(ob);
                //2.) first credit payment
                runningBal = CalculateRunningBalance(0, @event.TotalAmountOwed, runningBal);
                var cr = new CreateInitialLedgerEntry(@event.CustomerId, 0, @event.TotalAmountOwed, runningBal, "Total credit", @event.EventId, @event.SeasonId);
                await _createLedgerDebitEntryCH.HandleAsync(cr);
                //3.) first down payment
                runningBal = CalculateRunningBalance(@event.TotalAmountRepaid, 0, runningBal);
                var dr = new CreateInitialLedgerEntry(@event.CustomerId, @event.TotalAmountRepaid, 0, runningBal, "Total repaid", @event.EventId, @event.SeasonId);
                await _createLedgerDebitEntryCH.HandleAsync(dr);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
