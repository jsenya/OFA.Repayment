using OFA.Repayment.WM.Messages.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace OFA.Repayment.WM.Messages.Commands
{
    public interface ICommand : IMessage
    {
        IEvent @event { get; }
    }
}
