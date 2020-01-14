using OFA.Repayment.WM.Messages.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OFA.Repayment.WM.CommandHandlers.ICommandHandlers
{
    public interface ICustomerCommandHandler<TEvent> where TEvent : IEvent
    {
        Task AppendAsync(TEvent @event);
    }
}
