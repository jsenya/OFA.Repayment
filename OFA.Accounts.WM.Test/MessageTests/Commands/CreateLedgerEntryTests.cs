using OFA.Accounts.WM.Messages.Commands;
using OFA.Accounts.WM.Messages.Events;
using OFA.Common.Messages.Events;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace OFA.Accounts.WM.Test.MessageTests.Commands
{
    public class CreateLedgerEntryTests
    {
        [Fact]
        public void CreateCustomerCommandShouldReturnAnEvent()
        {
            //arrange
            CreateLedgerDebitEntry createCustomerCommand = new CreateLedgerDebitEntry(1, 1000, 0, 100, "test transaction", Guid.NewGuid(), 180);

            //act
            IEvent @event = createCustomerCommand.@event;

            //assert
            Assert.NotNull(@event);
            Assert.True(@event is LedgerDebitEntryCreated);
        }
    }
}
