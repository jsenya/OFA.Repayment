using OFA.Repayment.WM.Messages.Commands;
using OFA.Repayment.WM.Messages.Events;
using System;
using System.Collections.Generic;
using System.Linq;
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
            Season season = _data.Seasons.FirstOrDefault();
            CreateSeason createCustomerCommand = new CreateSeason(season.SeasonID, season.SeasonName, 
                DateTime.Parse(season.StartDate));

            //act
            IEvent @event = createCustomerCommand.@event;

            //assert
            Assert.NotNull(@event);
            Assert.True(@event is SeasonCreated);
        }
    }
}
