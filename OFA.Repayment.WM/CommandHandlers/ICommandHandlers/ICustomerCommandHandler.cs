using OFA.Common.Messages.Events;
using System.Threading.Tasks;

namespace OFA.Repayment.WM.CommandHandlers.ICommandHandlers
{
    public interface ICustomerCommandHandler<TEvent> where TEvent : IEvent
    {
        Task AppendAsync(TEvent @event);
    }
}
