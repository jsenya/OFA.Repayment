using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OFA.Accounts.WM.CommandHandlers.ICommandHandlers;
using OFA.Accounts.WM.Messages.Commands;
using static OFA.Accounts.WM.API.Helpers;

namespace OFA.Accounts.WM.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommandsController : ControllerBase
    {
        private readonly ICreateLedgerDebitEntryCommandHandler<CreateLedgerDebitEntry> _ledgerDebitCH;
        private readonly SampleData _sampleData;
        public CommandsController(ICreateLedgerDebitEntryCommandHandler<CreateLedgerDebitEntry> ledgerDebitCH)
        {
            _ledgerDebitCH = ledgerDebitCH;
            _sampleData = "sampledata.json".GetSampleData();
        }

        [HttpGet, Route("SimulatePayments")]
        public async Task<IActionResult> SimulatePayments()
        {
            foreach(var payment in _sampleData.RepaymentUploads)
            {
                var command = new CreateLedgerDebitEntry(payment.CustomerID, payment.Amount, 0, 0, "original payment", Guid.NewGuid(), 
                    payment.SeasonID > 0 ? (int?)payment.SeasonID : null);
                await _ledgerDebitCH.HandleAsync(command);
            }
            return Ok("payments have been streamed to the event store.");
        }
    }
}