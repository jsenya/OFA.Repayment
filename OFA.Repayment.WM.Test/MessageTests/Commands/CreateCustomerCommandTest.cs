using OFA.Repayment.WM.Messages.Commands;
using OFA.Repayment.WM.Messages.Events;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using static OFA.Repayment.WM.Test.Helpers;

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
            CreateCustomer createCustomerCommand = new CreateCustomer(1, "John Doe");

            //act
            IEvent @event = createCustomerCommand.@event;

            //assert
            Assert.NotNull(@event);
            Assert.True(@event is CustomerCreated);
        }
    }
}
