using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OFA.Accounts.WM.Messages.Events;
using OFA.Common;
using OFA.DAL.EventStore.DAL.IDAL;

namespace OFA.Accounts.WM.BgWorker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IOFAEventStore _eventStore;

        public Worker(ILogger<Worker> logger, IOFAEventStore eStore)
        {
            _logger = logger;
            _eventStore = eStore;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _eventStore.SubscribeToStreamAsync("customer-summary", "account-management");
            await _eventStore.SetListenerAsync("customer-summary", "account-management", (_, x) =>
            {
                var evt = x.Event.Data.FromBytes<CustomerSummaryCreated>();
                Console.WriteLine($"{evt.CustomerId} - {evt.SeasonId} - {evt.TotalAmountOwed} - {evt.TotalAmountRepaid}");
            });

            //while (!stoppingToken.IsCancellationRequested)
            //{
            //    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            //    await Task.Delay(1000, stoppingToken);
            //}
        }
    }
}
