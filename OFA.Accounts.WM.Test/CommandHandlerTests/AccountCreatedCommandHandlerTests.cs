using OFA.Accounts.WM.CommandHandlers.ICommandHandlers;
using OFA.Accounts.WM.Messages.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using static OFA.Accounts.WM.Test.Helpers;

namespace OFA.Accounts.WM.Test.CommandHandlerTests
{
    public class AccountCreatedCommandHandlerTests
    {
        private readonly ICreateAccountCommandHandler<CreateAccount> _handler;
        public AccountCreatedCommandHandlerTests()
        {
            _handler = GetAccountCreatedCommandHandler();
        }

        [Fact]
        public async Task HandleCreateAccountCommandShouldCompleteExecutingWithoutErrors()
        {
            //arrange
            CreateAccount command = new CreateAccount(3, 180, Guid.NewGuid());

            //act
            var _task = _handler.HandleAsync(command);
            await _task;

            //assert
            Assert.True(_task.IsCompleted);
        }

        [Fact]
        public async Task HandleCreateAccountDefaultedCommandShouldCompleteExecutingWithoutErrors()
        {
            //arrange
            CreateAccount command = new CreateAccount(4, 191, Guid.NewGuid());
            command.AccountStatus = AccountStatus.DEFAULTED.ToString();

            //act
            var _task = _handler.HandleAsync(command);
            await _task;

            //assert
            Assert.True(_task.IsCompleted);
        }
    }
}
