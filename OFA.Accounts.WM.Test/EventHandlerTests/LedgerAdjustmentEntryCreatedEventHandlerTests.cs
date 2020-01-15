using OFA.Accounts.WM.CommandHandlers.ICommandHandlers;
using OFA.Accounts.WM.EventHandlers.IEventHandlers;
using OFA.Accounts.WM.Messages.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using static OFA.Accounts.WM.Test.Helpers;

namespace OFA.Accounts.WM.Test.EventHandlerTests
{
    public class LedgerAdjustmentEntryCreatedEventHandlerTests
    {
        private readonly ILedgerAdjustmentEntryCreatedEventHandler<LedgerAdjustmentEntryCreated> _evHandler;
        public LedgerAdjustmentEntryCreatedEventHandlerTests()
        {
            _evHandler = GetLedgerAdjustmentEntryCreatedEventHandler();
        }

        [Fact]
        public async Task HandlerShouldCompleteExcecutionWithoutThrowingAnError()
        {
            //arrange
            LedgerAdjustmentEntryCreated @event = new LedgerAdjustmentEntryCreated(1, 140, 500, 0, -500, Guid.NewGuid());

            //act
            Task _task = _evHandler.HandleAsync(@event);
            await _task;

            //assert
            Assert.True(_task.IsCompleted);
        }
    }
}
