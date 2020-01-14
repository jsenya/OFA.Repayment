using OFA.Repayment.WM.CommandHandlers.ICommandHandlers;
using OFA.Repayment.WM.Messages.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using static OFA.Repayment.WM.Test.Helpers;

namespace OFA.Repayment.WM.Test.CommandHandlerTests
{
    public class SeasonCommandHandlerTests
    {
        private readonly ISeasonCommandHandler<CreateSeason> _handler;
        public SeasonCommandHandlerTests()
        {
            _handler = GetSeasonCommandHandler();
        }
        [Fact]
        public async Task HandleCreateSeasonCommandShouldCompleteExecutingWithoutErrors()
        {
            //arrange
            CreateSeason @event = new CreateSeason(3, $"season from handler - {DateTime.UtcNow}", 
                DateTime.UtcNow);

            //act
            var _task = _handler.HandleAsync(@event);
            await _task;

            //assert
            Assert.True(_task.IsCompleted);
        }
    }
}
