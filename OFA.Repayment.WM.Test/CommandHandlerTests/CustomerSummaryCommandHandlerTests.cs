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
    public class CustomerSummaryCommandHandlerTests
    {
        private readonly ICustomerSummaryCommandHandler<CreateCustomerSummary> _handler;
        public CustomerSummaryCommandHandlerTests()
        {
            _handler = GetCustomerSummaryCommandHandler();
        }

        [Fact]
        public async Task HandleCreateCustomerSummaryCommandShouldCompleteExecutingWithoutErrors()
        {
            //arrange
            CreateCustomerSummary command = new CreateCustomerSummary(54, 114, 26900, 1190);

            //act
            var _task = _handler.HandleAsync(command);
            await _task;

            //assert
            Assert.True(_task.IsCompleted);
        }
    }
}
