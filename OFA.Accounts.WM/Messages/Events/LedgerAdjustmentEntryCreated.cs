using OFA.Common.Messages.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace OFA.Accounts.WM.Messages.Events
{
    public class LedgerAdjustmentEntryCreated : IEvent
    {
        public Guid EventId { get; private set; }
        public Guid CorrelationId { get; set; }
        public string AccountName { get; set; }
        public int CustomerId { get; set; }
        public int? SeasonId { get; set; }
        public int Debit { get; set; }
        public int Credit { get; set; }
        public int Balance { get; set; }
        public string Details { get; set; }

        public LedgerAdjustmentEntryCreated(int custId, int seasonId, int debit, int credit, int balance, Guid correlationId)
        {
            if (custId == 0) throw new Exception("Customer id is invalid");
            if (credit < 0) throw new Exception("Invalid credit amount.");

            EventId = Guid.NewGuid();
            CorrelationId = correlationId;
            CustomerId = custId;
            SeasonId = seasonId;
            Credit = credit;
            Debit = debit;
            Details = "adjustment repayment";
            AccountName = $"{custId}/{seasonId.ToString() ?? "ANY"}";
            Balance = balance;
        }
    }
}
