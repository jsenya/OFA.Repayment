using OFA.Repayment.WM.CommandHandlers.ICommandHandlers;
using OFA.Repayment.WM.Messages.Commands;
using OFA.Repayment.WM.Messages.Events;
using System;
using System.Threading.Tasks;
using Xunit;
using static OFA.Repayment.WM.Test.Helpers;

namespace OFA.Repayment.WM.Test.CommandHandlerTests
{
    public class CustomerCommandHandlerTests
    {
        private readonly ICustomerCommandHandler<CreateCustomer> _handler;
        public CustomerCommandHandlerTests()
        {
            _handler = GetCustomerCommandHandler();
        }

        [Fact]
        public async Task AppendAsync()
        {
            //arrange
            CreateCustomer @event = new CreateCustomer(3, $"user from handler - {DateTime.UtcNow}");

            //act
            var _task = _handler.HandleAsync(@event);
            await _task;

            //assert
            Assert.True(_task.IsCompleted);
        }
    }
}
