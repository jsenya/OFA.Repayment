using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OFA.Accounts.WM.CommandHandlers;
using OFA.Accounts.WM.CommandHandlers.ICommandHandlers;
using OFA.Accounts.WM.Messages.Commands;
using OFA.Accounts.WM.Repositories;
using OFA.DAL.EventStore.DAL;
using OFA.DAL.EventStore.DAL.IDAL;

namespace OFA.Accounts.WM.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            var _evStore = new OFAEventStore("admin", "changeit", "localhost", "1113", "test_client");
            var _ledgerDebitCH = new CreateLedgerDebitEntryCommandHandler(new LedgerRepository(_evStore));

            services.AddSingleton<IOFAEventStore>(_evStore);
            services.AddSingleton<ICreateLedgerDebitEntryCommandHandler<CreateLedgerDebitEntry>>(_ledgerDebitCH);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
