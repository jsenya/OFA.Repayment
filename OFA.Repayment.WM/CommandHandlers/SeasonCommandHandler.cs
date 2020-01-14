using OFA.Repayment.WM.CommandHandlers.ICommandHandlers;
using OFA.Repayment.WM.Messages.Commands;
using OFA.Repayment.WM.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OFA.Repayment.WM.CommandHandlers
{
    public class SeasonCommandHandler : ISeasonCommandHandler<CreateSeason>
    {
        private readonly ISeasonRepository _repository;
        public SeasonCommandHandler(ISeasonRepository seasonRepository)
        {
            _repository = seasonRepository;
        }
        public async Task HandleAsync(CreateSeason command)
        {
            try
            {
                //since it's a 3rd party event we don't need to do any business logic just save the event
                await _repository.SaveAsync("seasons", command.@event);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
