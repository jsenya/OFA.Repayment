using System;
using System.Collections.Generic;
using System.Text;

namespace OFA.Repayment.WM.Messages.Events
{
    public interface IEvent : IMessage
    {
        Guid EventId { get; }
    }
}
