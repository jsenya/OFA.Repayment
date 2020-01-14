using OFA.Common.Messages.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace OFA.Common.Messages.Commands
{
    public interface ICommand : IMessage
    {
        IEvent @event { get; }
    }
}
