using OFA.Common.Messages.Events;
using OFA.Repayment.WM.Messages.Commands;
using OFA.Repayment.WM.Messages.Events;
using System.Linq;
using Xunit;

namespace OFA.Repayment.WM.Test.MessageTests.Commands
{
    public class CreateCustomerCommandTest
    {
        private readonly SampleData _data;
        public CreateCustomerCommandTest()
        {
            _data = "sampledata.json".GetSampleData();
        }

        [Fact]
        public void CreateCustomerCommandShouldReturnAnEvent()
        {
            //arrange
            Customer customer = _data.Customers.FirstOrDefault();
            CreateCustomer createCustomerCommand = new CreateCustomer(customer.CustomerID, customer.CustomerName);

            //act
            IEvent @event = createCustomerCommand.@event;

            //assert
            Assert.NotNull(@event);
            Assert.True(@event is CustomerCreated);
        }
    }
}
