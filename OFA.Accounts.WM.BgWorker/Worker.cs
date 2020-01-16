using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using OFA.Accounts.WM.CommandHandlers.ICommandHandlers;
using OFA.Accounts.WM.EventHandlers.IEventHandlers;
using OFA.Accounts.WM.Messages.Commands;
using OFA.Accounts.WM.Messages.Events;
using OFA.Common;
using OFA.DAL.EventStore.DAL.IDAL;

namespace OFA.Accounts.WM.BgWorker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IOFAEventStore _eventStore;
        private readonly ICustomerSummaryCreatedEventHandler<CustomerSummaryCreated> _custSummaryCreatedEH;
        private readonly ILedgerAdjustmentEntryCreatedEventHandler<LedgerAdjustmentEntryCreated> _adjustmentEH;

        public Worker(ILogger<Worker> logger, IOFAEventStore eStore, ICustomerSummaryCreatedEventHandler<CustomerSummaryCreated> custSummaryCreatedEH, ILedgerAdjustmentEntryCreatedEventHandler<LedgerAdjustmentEntryCreated> adjustmentEH)
        {
            _logger = logger;
            _eventStore = eStore;
            _custSummaryCreatedEH = custSummaryCreatedEH;
            _adjustmentEH = adjustmentEH;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            //subscribe to customer summary events
            var subscriptionCreated = await _eventStore.SubscribeToStreamAsync("customer-summary", "gl-manager");
            var subscription2Created = await _eventStore.SubscribeToStreamAsync("loan-ledger", "loan-manager");

            await _eventStore.SetListenerAsync("customer-summary", "gl-manager", async (_, x) =>
            {
                var evt = x.Event.Data.FromBytes<CustomerSummaryCreated>();
                Console.WriteLine($"processing @ {DateTime.UtcNow} \n-> {JsonConvert.SerializeObject(evt)}");
                await _custSummaryCreatedEH.HandlerAsync(evt);
            });

            await _eventStore.SetListenerAsync("loan-ledger", "loan-manager", async (_, x) =>
            {
                if(x.Event.EventType == "LedgerAdjustmentEntryCreated")
                {
                    var evt = x.Event.Data.FromBytes<LedgerAdjustmentEntryCreated>();
                    Console.WriteLine($"processing @ {DateTime.UtcNow} \n-> {JsonConvert.SerializeObject(evt)}");
                    await _adjustmentEH.HandleAsync(evt);
                }
            });
        }
    }
}
