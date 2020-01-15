using OFA.Accounts.WM.Messages.Events;
using OFA.Common.Messages.Commands;
using OFA.Common.Messages.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace OFA.Accounts.WM.Messages.Commands
{
    public class CreateAccount : ICommand
    {
        public IEvent @event => Apply();
        public string AccountNumber { get; set; }
        public string AccountName { get; set; }
        public int CustomerId { get; set; }
        public int SeasonId { get; set; }
        public string AccountStatus { get; set; }
        public CreateAccount(int customerId, int seasonId)
        {
            if (customerId == 0) throw new Exception("Customer id is invalid.");
            if (seasonId == 0) throw new Exception("Season id is invalid.");

            CustomerId = customerId;
            SeasonId = seasonId;
            AccountNumber = $"{customerId}/{seasonId}";
            AccountName = $"{customerId}/{seasonId}";
            AccountStatus = OFA.Accounts.WM.AccountStatus.PENDING.ToString();
        }

        private IEvent Apply()
        {
            try
            {
                return new AccountCreated(CustomerId, SeasonId, AccountName, AccountNumber, AccountStatus);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
