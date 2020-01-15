using OFA.Accounts.WM.CommandHandlers.ICommandHandlers;
using OFA.Accounts.WM.Messages.Commands;
using OFA.Accounts.WM.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OFA.Accounts.WM.CommandHandlers
{
    public class CreateInitialLedgerEntryCommandHandler : ICreateInitialLedgerEntryCommandHandler<CreateInitialLedgerEntry>
    {
        private readonly ILedgerRepository _repository;
        public CreateInitialLedgerEntryCommandHandler(ILedgerRepository repository)
        {
            _repository = repository;
        }
        public async Task HandleAsync(CreateInitialLedgerEntry command)
        {
            try
            {
                await _repository.SaveAsync("loan-ledger", command.@event);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
