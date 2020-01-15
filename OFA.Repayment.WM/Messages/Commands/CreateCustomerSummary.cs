using OFA.Common.Messages.Commands;
using OFA.Common.Messages.Events;
using OFA.Repayment.WM.Messages.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace OFA.Repayment.WM.Messages.Commands
{
    public class CreateCustomerSummary : ICommand
    {
        public IEvent @event => Apply();
        public int CustomerId { get; set; }
        public int SeasonId { get; set; }
        public int TotalAmountOwed { get; set; }
        public int TotalAmountRepaid { get; set; }

        public CreateCustomerSummary(int customerId, int seasonId, int totalOwed, int totalRepaid)
        {
            if (customerId == 0) throw new Exception("Customer id is invalid.");
            if (seasonId == 0) throw new Exception("Season id is invalid.");
            if (totalOwed < 0) throw new Exception("Owed amount is invalid.");
            if (totalRepaid < 0) throw new Exception("Repaid amount is invalid.");

            CustomerId = customerId;
            SeasonId = seasonId;
            TotalAmountOwed = totalOwed;
            TotalAmountRepaid = totalRepaid;
        }

        private IEvent Apply()
        {
            try
            {
                return new CustomerSummaryCreated(CustomerId, SeasonId, TotalAmountOwed, TotalAmountRepaid);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
