using OFA.Accounts.WM.EventHandlers.IEventHandlers;
using OFA.Accounts.WM.Messages.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using static OFA.Accounts.WM.Test.Helpers;

namespace OFA.Accounts.WM.Test.CommandHandlerTests
{
    public class CreateLedgerEntryCommandHandlerTest
    {
        private readonly ICreateLedgerEntryCommandHandler<CreateLedgerDebitEntry> _handler;
        public CreateLedgerEntryCommandHandlerTest()
        {
            _handler = GetCreateLedgerEntryCommandHandler();
        }

        [Fact]
        public async Task HandleCreateAccountCommandShouldCompleteExecutingWithoutErrors()
        {
            //arrange
            CreateLedgerDebitEntry command = new CreateLedgerDebitEntry(3, 180, 0, Guid.NewGuid(), 180);

            //act
            var _task = _handler.HandleAsync(command);
            await _task;

            //assert
            Assert.True(_task.IsCompleted);
        }
    }
}
