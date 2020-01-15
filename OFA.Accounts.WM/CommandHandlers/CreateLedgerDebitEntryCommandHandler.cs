using OFA.Accounts.WM.EventHandlers.IEventHandlers;
using OFA.Accounts.WM.Messages.Commands;
using OFA.Accounts.WM.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OFA.Accounts.WM.CommandHandlers
{
    public class CreateLedgerDebitEntryCommandHandler : ICreateLedgerEntryCommandHandler<CreateLedgerDebitEntry>
    {
        private readonly ILedgerRepository _repository;
        public CreateLedgerDebitEntryCommandHandler(ILedgerRepository repository)
        {
            _repository = repository;
        }
        public async Task HandleAsync(CreateLedgerDebitEntry command)
        {
            try
            {
                //1. get the oldest season
                //2. get the season account
                //3. get the latest gl entry for the season account
                //4. create entry
                //5. if balance is less than zero mark current season as repaid and create a new debit entry
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
