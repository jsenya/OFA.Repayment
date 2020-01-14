using OFA.Common.Messages.Events;
using System;

namespace OFA.Repayment.WM.Messages.Events
{
    public class CustomerCreated : IEvent
    {
        public Guid EventId { get; private set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }

        public CustomerCreated(int custId, string custName)
        {
            EventId = Guid.NewGuid();
            CustomerId = custId;
            CustomerName = custName;
        }
    }
}
