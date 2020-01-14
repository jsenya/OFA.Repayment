using OFA.Common.Messages.Commands;
using System.Threading.Tasks;

namespace OFA.Repayment.WM.CommandHandlers.ICommandHandlers
{
    public interface ICustomerCommandHandler<TCommand> where TCommand : ICommand
    {
        Task HandleAsync(TCommand @event);
    }
}
