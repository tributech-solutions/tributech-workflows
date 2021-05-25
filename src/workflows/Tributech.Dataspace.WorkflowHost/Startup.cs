using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hangfire;
using Hangfire.Dashboard;
using Hangfire.PostgreSql;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Tributech.Dataspace.WorkflowHost.Infrastructure;
using Tributech.Dataspace.WorkflowHost.Infrastructure.Extensions;
using Tributech.Dataspace.WorkflowHost.Infrastructure.Registrations;

namespace Tributech.Dataspace.WorkflowHost {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {

            services.AddLogging();

            services.AddControllers();

            services.AddWorkflow(
                x =>
                    x.UsePostgreSQL(Configuration.GetConnectionString("Workflows"), true, true)
            );

            services
                .EnsureHangfireDB(Configuration.GetConnectionString("Master"), Configuration.GetConnectionString("Jobs"))
                .AddHangfire(configuration => configuration
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UsePostgreSqlStorage(Configuration.GetConnectionString("Jobs")));

            // Add the processing server as IHostedService
            services.AddHangfireServer();

            // Add framework services.
            services.AddMvc();

            services
                .RegisterCoreComponents(Configuration)
                .RegisterWorkflowSteps(Configuration)
                .RegisterJobs(Configuration)
                .RegisterServices(Configuration);

            services.AddHealthChecks();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {

            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/health").RequireHost("*:5050");
                endpoints.MapHangfireDashboardWithNoAuthorization();
            });

            app
                .StartWorkflowHost(Configuration)
                .StartJobs();


        }
    }
}
