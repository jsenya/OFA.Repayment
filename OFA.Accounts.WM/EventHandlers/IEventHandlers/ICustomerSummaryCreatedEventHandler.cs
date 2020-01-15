using OFA.Common.Messages.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OFA.Accounts.WM.EventHandlers.IEventHandlers
{
    public interface ICustomerSummaryCreatedEventHandler<TEvent> where TEvent : IEvent
    {
        Task HandlerAsync(TEvent @event);
    }
}
