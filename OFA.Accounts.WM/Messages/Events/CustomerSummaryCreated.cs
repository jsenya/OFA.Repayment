using OFA.Common.Messages.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace OFA.Accounts.WM.Messages.Events
{
    public class CustomerSummaryCreated : IEvent
    {
        public Guid EventId { get; private set; }
        public int CustomerId { get; set; }
        public int SeasonId { get; set; }
        public int TotalAmountOwed { get; set; }
        public int TotalAmountRepaid { get; set; }
    }
}
