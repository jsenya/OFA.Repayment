using OFA.Accounts.WM.Messages.Events;
using OFA.Common.Messages.Commands;
using OFA.Common.Messages.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace OFA.Accounts.WM.Messages.Commands
{
    public class CreateInitialLedgerEntry : ICommand
    {
        public IEvent @event => Apply();
        public Guid CorrelationId { get; set; }
        public string AccountName { get; set; }
        public int CustomerId { get; set; }
        public int? SeasonId { get; set; }
        public int Debit { get; set; }
        public int Credit { get; set; }
        public int Balance { get; set; }
        public string Details { get; set; }
        public CreateInitialLedgerEntry(int custId, int debit, int credit, string details, Guid correlationId, int? seasonId = null)
        {
            if (custId == 0) throw new Exception("Customer id is invalid");
            if (debit < 0) throw new Exception("Invalid debit amount.");
            if (credit < 0) throw new Exception("Invalid credit amount.");

            CorrelationId = correlationId;
            CustomerId = custId;
            SeasonId = seasonId;
            Debit = debit;
            Credit = credit;
            Details = details;
        }
        private IEvent Apply()
        {
            try
            {
                return new InitialLedgerEntryCreated(CustomerId, (int)SeasonId, AccountName, Debit, Credit, Details, CorrelationId);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
