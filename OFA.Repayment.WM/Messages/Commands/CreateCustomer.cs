using OFA.Common.Messages.Commands;
using OFA.Common.Messages.Events;
using OFA.Repayment.WM.Messages.Events;
using System;

namespace OFA.Repayment.WM.Messages.Commands
{
    public class CreateCustomer : ICommand
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public IEvent @event => Apply();

        private IEvent Apply()
        {
            try
            {
                return new CustomerCreated(CustomerId, CustomerName);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public CreateCustomer(int id, string name)
        {
            if (id == 0) throw new Exception("Customer id is invalid.");
            if (string.IsNullOrEmpty(name)) throw new ArgumentNullException("Customer name is missing.");

            CustomerId = id;
            CustomerName = name;
        }
    }
}
