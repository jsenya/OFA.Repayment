using OFA.Common.Messages.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace OFA.Accounts.WM.Messages.Events
{
    public class AccountCreated : IEvent
    {
        public Guid EventId { get; set; }
        public string AccountNumber { get; set; }
        public string AccountName { get; set; }
        public int CustomerId { get; set; }
        public int SeasonId { get; set; }
        public string AccountStatus { get; set; }
        public AccountCreated(int customerId, int seasonId, string accName, string accNo, string status)
        {
            EventId = Guid.NewGuid();
            CustomerId = customerId;
            SeasonId = seasonId;
            AccountNumber = accNo;
            AccountName = accName;
            AccountStatus = status;
        }
    }
}
