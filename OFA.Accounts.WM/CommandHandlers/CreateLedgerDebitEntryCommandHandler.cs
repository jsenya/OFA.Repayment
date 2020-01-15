using OFA.Accounts.WM.CommandHandlers.ICommandHandlers;
using OFA.Accounts.WM.EventHandlers.IEventHandlers;
using OFA.Accounts.WM.Messages.Commands;
using OFA.Accounts.WM.Messages.Events;
using OFA.Accounts.WM.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static OFA.Accounts.WM.Helpers;

namespace OFA.Accounts.WM.CommandHandlers
{
    public class CreateLedgerDebitEntryCommandHandler : ICreateLedgerDebitEntryCommandHandler<CreateLedgerDebitEntry>
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
                string projectionName = $"pendingAccounts-{command.CustomerId}";
                string _query = "fromStream('loan-ledger') .when({ $init: function(){ return { items: [] } }, $any: function(s,e){ let entry = e.body; if(entry.CustomerId === " + command.CustomerId + ") { let index = s.items.map(function(e) { return e.CustomerId+'/'+e.SeasonId; }) .indexOf(entry.CustomerId+'/'+entry.SeasonId); let status = 'PENDING'; if(entry.Balance === 0) status = 'REPAID'; else if(entry.Balance < 0) status = 'ADJUSTMENT'; else if(entry.Balance > 0) statuse = 'PENDING'; if(status !== 'REPAID') { if(index !== -1) { s.items[index].Balance = entry.Balance; s.items[index].AccountStatus = status; } else { s.items.push({ AccountStatus: status, CustomerId: entry.CustomerId, SeasonId: entry.SeasonId, Balance: entry.Balance }); } } else { if(index !== -1) { s.items.splice(index, 1); } } } s.items.sort((a,b)=> a.SeasonId > b.SeasonId ? 1 : -1); } });";
                await _repository.CreateProjectionAsync(projectionName, _query);

                //1. get the oldest PENDING season entry from ledger
                var _entry = await _repository.GetPendingEntriesAsync(projectionName);

                //2. create entry
                int balance = 0;
                if(_entry.items.Length > 0)
                {
                    balance = CalculateRunningBalance(command.Debit, command.Credit, _entry.items[0].Balance);
                    command.SeasonId = command.SeasonId ?? _entry.items[0].SeasonId;
                }

                if(balance >= 0)
                    command.Balance = balance;

                await _repository.SaveAsync("loan-ledger", command.@event);

                //5. if balance is less than zero create a readjustment debit entry for the same season
                if (balance < 0)
                {
                    var readjustment = new CreateLedgerAdjustmentEntry(command.CustomerId, (int)command.SeasonId, Math.Abs(balance), 0, CalculateRunningBalance(Math.Abs(balance), 0, command.Balance), command.@event.EventId);
                    await _repository.SaveAsync("loan-ledger", readjustment.@event);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
