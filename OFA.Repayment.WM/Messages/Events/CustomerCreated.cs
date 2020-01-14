using EventStore.ClientAPI;
using System;
using System.Collections.Generic;
using System.Text;

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
