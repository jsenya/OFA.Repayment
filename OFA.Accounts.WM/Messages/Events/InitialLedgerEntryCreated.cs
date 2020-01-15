using OFA.Common.Messages.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace OFA.Accounts.WM.Messages.Events
{
    public class InitialLedgerEntryCreated : IEvent
    {
        public Guid EventId { get; private set; }
        public Guid CorrelationId { get; set; }
        public string AccountName { get; set; }
        public int CustomerId { get; set; }
        public int? SeasonId { get; set; }
        public string Details { get; set; }
        public int Debit { get; set; }
        public int Credit { get; set; }
        public int Balance { get; set; }
        public InitialLedgerEntryCreated(int custId, int seasonId, string accName, int debit, int credit, int balance, string details, Guid correlationId)
        {
            EventId = Guid.NewGuid();
            CorrelationId = correlationId;
            CustomerId = custId;
            SeasonId = seasonId;
            Debit = debit;
            Credit = credit;
            AccountName = accName;
            Details = details;
            Balance = balance;
        }
    }
}
