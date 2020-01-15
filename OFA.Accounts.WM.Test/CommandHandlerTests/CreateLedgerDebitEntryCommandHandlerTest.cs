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
    public class CreateLedgerDebitEntryCommandHandlerTest
    {
        private readonly ICreateLedgerDebitEntryCommandHandler<CreateLedgerDebitEntry> _handler;
        private readonly ICreateInitialLedgerEntryCommandHandler<CreateInitialLedgerEntry> _handler2;
        public CreateLedgerDebitEntryCommandHandlerTest()
        {
            _handler = GetCreateLedgerDebitEntryCommandHandler();
            _handler2 = GetCreateInitialLedgerEntryCommandHandler();
        }

        [Fact]
        public async Task HandleCreateGLDebitEntryCommandShouldCompleteExecutingWithoutErrors()
        {
            //arrange
            var command = new CreateLedgerDebitEntry(14, 2750, 0, "loan repayment", Guid.NewGuid(), 180);

            //act
            var _task = _handler.HandleAsync(command);
            await _task;

            //assert
            Assert.True(_task.IsCompleted);
        }

        [Fact]
        public async Task HandleCreateInitialGLEntryCommandShouldCompleteExecutingWithoutErrors()
        {
            //arrange
            var command = new CreateInitialLedgerEntry(14, 2750, 0, "Opening Balance", Guid.NewGuid(), 180);

            //act
            var _task = _handler2.HandleAsync(command);
            await _task;

            //assert
            Assert.True(_task.IsCompleted);
        }
    }
}
