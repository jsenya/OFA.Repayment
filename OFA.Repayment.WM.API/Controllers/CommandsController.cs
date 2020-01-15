using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OFA.Repayment.WM.CommandHandlers.ICommandHandlers;
using OFA.Repayment.WM.Messages.Commands;
using static OFA.Repayment.WM.API.Helpers;

namespace OFA.Repayment.WM.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommandsController : ControllerBase
    {
        private readonly ICustomerCommandHandler<CreateCustomer> _customerCH;
        private readonly ISeasonCommandHandler<CreateSeason> _seasonCH;
        private readonly ICustomerSummaryCommandHandler<CreateCustomerSummary> _custSummaryCH;
        private readonly SampleData _sampleData;
        public CommandsController(ICustomerCommandHandler<CreateCustomer> custCH, ISeasonCommandHandler<CreateSeason> seasCH, ICustomerSummaryCommandHandler<CreateCustomerSummary> custSumCH)
        {
            _customerCH = custCH;
            _seasonCH = seasCH;
            _custSummaryCH = custSumCH;
            _sampleData = "sampledata.json".GetSampleData();
        }

        [HttpGet, Route("SeedData")]
        public async Task<IActionResult> SeedData()
        {
            foreach (var customer in _sampleData.Customers)
            {
                CreateCustomer command = new CreateCustomer(customer.CustomerID, customer.CustomerName);
                await _customerCH.HandleAsync(command);
            }

            //create seasons
            foreach (var season in _sampleData.Seasons)
            {
                CreateSeason command = new CreateSeason(season.SeasonID, season.SeasonName,
                DateTime.Parse(season.StartDate));
                await _seasonCH.HandleAsync(command);
            }

            //create summaries
            foreach (var customerSummary in _sampleData.CustomerSummaries)
            {
                CreateCustomerSummary command = new CreateCustomerSummary(customerSummary.CustomerID, customerSummary.SeasonID, customerSummary.Credit, customerSummary.TotalRepaid);
                await _custSummaryCH.HandleAsync(command);
            }


            return Ok("successfully simulated data upload, check the event store.");
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