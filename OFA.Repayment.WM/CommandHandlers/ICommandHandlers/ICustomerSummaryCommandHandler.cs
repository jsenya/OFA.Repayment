using OFA.Common.Messages.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OFA.Repayment.WM.CommandHandlers.ICommandHandlers
{
    public interface ICustomerSummaryCommandHandler<TCommand> where TCommand : ICommand
    {
        Task HandleAsync(TCommand command);
     }
}
