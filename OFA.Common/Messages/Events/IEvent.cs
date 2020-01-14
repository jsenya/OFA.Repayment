using System;

namespace OFA.Common.Messages.Events
{
    public interface IEvent : IMessage
    {
        Guid EventId { get; }
    }
}
