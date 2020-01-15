using OFA.Accounts.WM.CommandHandlers.ICommandHandlers;
using OFA.Accounts.WM.Messages.Commands;
using OFA.Accounts.WM.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OFA.Accounts.WM.CommandHandlers
{
    public class AccountCreatedCommandHandler : IAccountCreatedCommandHandler<CreateAccount>
    {
        private readonly IAccountRepository _repository;
        public AccountCreatedCommandHandler(IAccountRepository repository)
        {
            _repository = repository;
        }
        public async Task HandleAsync(CreateAccount command)
        {
            try
            {
                //since it's a 3rd party event we don't need to do any business logic just save the event
                await _repository.SaveAsync("accounts", command.@event);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
