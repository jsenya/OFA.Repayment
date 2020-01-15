using OFA.Accounts.WM.EventHandlers.IEventHandlers;
using OFA.Accounts.WM.Messages.Commands;
using OFA.Accounts.WM.Messages.Events;
using OFA.Accounts.WM.Projections;
using OFA.Accounts.WM.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static OFA.Accounts.WM.Helpers;

namespace OFA.Accounts.WM.EventHandlers
{
    public class LedgerAdjustmentEntryCreatedEventHandler : ILedgerAdjustmentEntryCreatedEventHandler<LedgerAdjustmentEntryCreated>
    {
        private readonly ILedgerRepository _repository;
        public LedgerAdjustmentEntryCreatedEventHandler(ILedgerRepository repository)
        {
            _repository = repository;
        }
        public async Task HandleAsync(LedgerAdjustmentEntryCreated @event)
        {
            try
            {
                string projectionName = $"adjustedAccounts-{@event.CustomerId}";
                string _query = "fromStream('loan-ledger') .when({ $init: function(){ return { items: [] } }, $any: function(s,e){ let entry = e.body; if(entry.CustomerId === " + @event.CustomerId + ") { let index = s.items.map(function(e) { return e.CustomerId+'/'+e.SeasonId; }) .indexOf(entry.CustomerId+'/'+entry.SeasonId); let status = 'PENDING'; if(entry.Balance === 0) status = 'REPAID'; else if(entry.Balance < 0) status = 'ADJUSTMENT'; else if(entry.Balance > 0) statuse = 'PENDING'; if(entry.Balance < 0) { if(index !== -1) { s.items[index].Balance = entry.Balance; s.items[index].AccountStatus = status; } else { s.items.push({ AccountStatus: status, CustomerId: entry.CustomerId, SeasonId: entry.SeasonId, Debit: entry.Debit, Credit: entry.Credit, Balance: entry.Balance }); } } else { if(index !== -1) { s.items.splice(index, 1); } } } s.items.sort((a,b)=> a.SeasonId > b.SeasonId ? 1 : -1); } });";
                await _repository.CreateProjectionAsync(projectionName, _query);

                //1. read and get all adjusted entries
                var _entry = await _repository.GetPendingEntriesAsync(projectionName);

                Entry adjustment = null;

                if (_entry.items.Length > 0)
                {
                    adjustment = _entry.items[0];

                    //2. first create a debit account to balance the adjustment
                    var readjustmentBalancer = new CreateLedgerAdjustmentEntry(adjustment.CustomerId, adjustment.SeasonId, 0, Math.Abs(adjustment.Balance), 0, @event.EventId);
                    await _repository.SaveAsync("loan-ledger", readjustmentBalancer.@event);
                }

                if(adjustment != null)
                {
                    //3. get next season and debit it **if any
                    var _pendingEntry = await _repository.GetPendingEntriesAsync($"pendingAccounts-{@event.CustomerId}");
                    if (_pendingEntry.items.Length > 0)
                    {
                        int debitAmount = Math.Abs(adjustment.Balance);
                        int balance = CalculateRunningBalance(debitAmount, 0, _pendingEntry.items[0].Balance);
                        var command = new CreateLedgerDebitEntry(adjustment.CustomerId, debitAmount, 0, balance, "adjustment repayment", @event.EventId, _pendingEntry.items[0].SeasonId);

                        await _repository.SaveAsync("loan-ledger", command.@event);
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
