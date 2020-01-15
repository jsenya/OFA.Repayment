using OFA.Accounts.WM.EventHandlers.IEventHandlers;
using OFA.Accounts.WM.Messages.Commands;
using OFA.Accounts.WM.Messages.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OFA.Accounts.WM.EventHandlers
{
    public class AccountCreatedEventHandler : IAccountCreatedEventHandler<AccountCreated>
    {
        private readonly ICreateLedgerEntryCommandHandler<CreateLedgerDebitEntry> _createLdgEntryCH;
        public AccountCreatedEventHandler(ICreateLedgerEntryCommandHandler<CreateLedgerDebitEntry> createLdgEntryCH)
        {
            _createLdgEntryCH = createLdgEntryCH;
        }
        public async Task HandleAsync(AccountCreated @event)
        {
            throw new NotImplementedException();
        }
    }
}
