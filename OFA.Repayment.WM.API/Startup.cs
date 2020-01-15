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
using OFA.DAL.EventStore.DAL;
using OFA.DAL.EventStore.DAL.IDAL;
using OFA.Repayment.WM.CommandHandlers;
using OFA.Repayment.WM.CommandHandlers.ICommandHandlers;
using OFA.Repayment.WM.Messages.Commands;
using OFA.Repayment.WM.Repositories;

namespace OFA.Repayment.WM.API
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
            var _custCH = new CustomerCommandHandler(new CustomerRepository(_evStore));
            var _seasonCH = new SeasonCommandHandler(new SeasonRepository(_evStore));
            var _custSummaryCH = new CustomerSummaryCommandHandler(new CustomerSummaryRepository(_evStore));

            services.AddSingleton<IOFAEventStore>(_evStore);
            services.AddSingleton<ICustomerCommandHandler<CreateCustomer>>(_custCH);
            services.AddSingleton<ISeasonCommandHandler<CreateSeason>>(_seasonCH);
            services.AddSingleton<ICustomerSummaryCommandHandler<CreateCustomerSummary>>(_custSummaryCH);
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
