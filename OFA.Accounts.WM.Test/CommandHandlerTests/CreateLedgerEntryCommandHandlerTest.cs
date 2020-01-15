using OFA.Accounts.WM.CommandHandlers.ICommandHandlers;
using OFA.Accounts.WM.EventHandlers.IEventHandlers;
using OFA.Accounts.WM.Messages.Commands;
using OFA.Accounts.WM.Messages.Events;
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
        private readonly ICreateLedgerDebitEntryCommandHandler<CreateLedgerDebitEntry> _handler;
        public CreateLedgerEntryCommandHandlerTest()
        {
            _handler = GetCreateLedgerDebitEntryCommandHandler();
        }

        [Fact]
        public async Task HandleCreateAccountCommandShouldCompleteExecutingWithoutErrors()
        {
            //arrange
            //CreateLedgerDebitEntry command = new CreateLedgerDebitEntry(3, 180, 0, Guid.NewGuid(), 180);
            var command = new CreateLedgerDebitEntry(14, 2750, 0, "Opening Balance", Guid.NewGuid(), 180);

            //act
            var _task = _handler.HandleAsync(command);
            await _task;

            //assert
            Assert.True(_task.IsCompleted);
        }
    }
}
