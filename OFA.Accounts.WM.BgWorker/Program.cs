using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OFA.Accounts.WM.CommandHandlers;
using OFA.Accounts.WM.CommandHandlers.ICommandHandlers;
using OFA.Accounts.WM.EventHandlers;
using OFA.Accounts.WM.EventHandlers.IEventHandlers;
using OFA.Accounts.WM.Messages.Commands;
using OFA.Accounts.WM.Messages.Events;
using OFA.Accounts.WM.Repositories;
using OFA.DAL.EventStore.DAL;
using OFA.DAL.EventStore.DAL.IDAL;

namespace OFA.Accounts.WM.BgWorker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    var _evStore = new OFAEventStore("admin", "changeit", "localhost", "1113", "test_client");
                    var _initialLedger = new CreateInitialLedgerEntryCommandHandler(new LedgerRepository(_evStore));
                    var _custSummaryEH = new CustomerSummaryCreatedEventHandler(_initialLedger);

                    services.AddHostedService<Worker>();
                    services.AddSingleton<IOFAEventStore>(_evStore);
                    services.AddSingleton<ICustomerSummaryCreatedEventHandler<CustomerSummaryCreated>>(_custSummaryEH);
                });
    }
}
