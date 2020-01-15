using OFA.Common.Messages.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace OFA.Repayment.WM.Messages.Events
{
    public class CustomerSummaryCreated : IEvent
    {
        public Guid EventId { get; private set; }
        public int CustomerId { get; set; }
        public int SeasonId { get; set; }
        public int TotalAmountOwed { get; set; }
        public int TotalAmountRepaid { get; set; }

        public CustomerSummaryCreated(int customerId, int seasonId, int totalOwed, int totalRepaid)
        {
            EventId = Guid.NewGuid();
            CustomerId = customerId;
            SeasonId = seasonId;
            TotalAmountOwed = totalOwed;
            TotalAmountRepaid = totalRepaid;
        }
    }
}
