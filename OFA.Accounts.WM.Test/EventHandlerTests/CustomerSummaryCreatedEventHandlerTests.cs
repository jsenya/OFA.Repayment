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
    public class CustomerSummaryCreatedEventHandlerTests
    {
        private readonly ICustomerSummaryCreatedEventHandler<CustomerSummaryCreated> _evHandler;
        public CustomerSummaryCreatedEventHandlerTests()
        {
            _evHandler = GetCustomerSummaryCreatedEventHandler();
        }

        [Fact]
        public async Task HandlerShouldCompleteExcecutionWithoutThrowingAnError()
        {
            //arrange
            CustomerSummaryCreated @event = new CustomerSummaryCreated
            {
                CustomerId = 1,
                SeasonId = 180,
                TotalAmountOwed = 1000,
                TotalAmountRepaid = 200
            };

            //act
            Task _task = _evHandler.HandlerAsync(@event);
            await _task;

            //assert
            Assert.True(_task.IsCompleted);
        }
    }
}
