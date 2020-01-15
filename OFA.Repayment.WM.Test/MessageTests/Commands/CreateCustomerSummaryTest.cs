using OFA.Common.Messages.Events;
using OFA.Repayment.WM.Messages.Commands;
using OFA.Repayment.WM.Messages.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace OFA.Repayment.WM.Test.MessageTests.Commands
{
    public class CreateCustomerSummaryTest
    {
        private readonly SampleData _data;
        public CreateCustomerSummaryTest()
        {
            _data = "sampledata.json".GetSampleData();
        }

        [Fact]
        public void CreateCustomerSummaryCommandShouldReturnAnEvent()
        {
            //arrange
            Customersummary customerSummary = _data.CustomerSummaries.FirstOrDefault();
            CreateCustomerSummary createCustomerCommand = new CreateCustomerSummary(customerSummary.CustomerID, customerSummary.SeasonID, customerSummary.Credit, customerSummary.TotalRepaid);

            //act
            IEvent @event = createCustomerCommand.@event;

            //assert
            Assert.NotNull(@event);
            Assert.True(@event is CustomerSummaryCreated);
        }
    }
}
