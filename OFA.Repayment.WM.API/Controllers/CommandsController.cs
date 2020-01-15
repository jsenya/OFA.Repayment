using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OFA.Repayment.WM.CommandHandlers.ICommandHandlers;
using OFA.Repayment.WM.Messages.Commands;

namespace OFA.Repayment.WM.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommandsController : ControllerBase
    {
        private readonly ICustomerCommandHandler<CreateCustomer> _customerCH;
        private readonly ISeasonCommandHandler<CreateSeason> _seasonCH;
        private readonly ICustomerSummaryCommandHandler<CreateCustomerSummary> _custSummaryCH;
        public CommandsController(ICustomerCommandHandler<CreateCustomer> custCH, ISeasonCommandHandler<CreateSeason> seasCH, ICustomerSummaryCommandHandler<CreateCustomerSummary> custSumCH)
        {
            _customerCH = custCH;
            _seasonCH = seasCH;
            _custSummaryCH = custSumCH;
        }

        [HttpPost, Route("CreateCustomers")]
        public async Task<IActionResult> CreateCustomers(List<CreateCustomer> commands)
        {
            foreach(var command in commands)
                await _customerCH.HandleAsync(command);
            return Ok();
        }

        [HttpPost, Route("CreateSeason")]
        public async Task<IActionResult> CreateSeason(List<CreateSeason> commands)
        {
            foreach (var command in commands)
                await _seasonCH.HandleAsync(command);
            return Ok();
        }

        [HttpPost, Route("CreateSummary")]
        public async Task<IActionResult> CreateSummary(List<CreateCustomerSummary> commands)
        {
            foreach (var command in commands)
                await _custSummaryCH.HandleAsync(command);
            return Ok();
        }
    }
}