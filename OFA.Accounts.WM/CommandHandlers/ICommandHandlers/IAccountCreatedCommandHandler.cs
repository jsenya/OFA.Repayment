using OFA.Accounts.WM.Messages.Commands;
using OFA.Common.Handlers;
using OFA.Common.Messages.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OFA.Accounts.WM.CommandHandlers.ICommandHandlers
{
    public interface IAccountCreatedCommandHandler<TCommand> where TCommand : ICommand
    {
        Task HandleAsync(TCommand command);
    }
}
