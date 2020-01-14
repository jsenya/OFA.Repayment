using OFA.Repayment.WM.Messages.Commands;
using OFA.Repayment.WM.Messages.Events;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace OFA.Repayment.WM.Test.MessageTests.Commands
{
    public class CreateSeasonCommandTest
    {
        private readonly SampleData _data;
        public CreateSeasonCommandTest()
        {
            _data = "sampledata.json".GetSampleData();
        }

        [Fact]
        public void CreateSeasonCommandShouldReturnAnEvent()
        {
            //arrange
            CreateSeason createCustomerCommand = new CreateSeason(1, "Summer 2020", DateTime.Now.AddDays(30));

            //act
            IEvent @event = createCustomerCommand.@event;

            //assert
            Assert.NotNull(@event);
            Assert.True(@event is SeasonCreated);
        }
    }
}
