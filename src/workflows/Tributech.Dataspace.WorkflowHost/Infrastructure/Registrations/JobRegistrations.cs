using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using Tributech.Dataspace.Workflow.Common;
using Tributech.Dataspace.Workflow.Example.Infrastructure.Jobs;

namespace Tributech.Dataspace.WorkflowHost.Infrastructure.Registrations
{
    public static class JobRegistrations {

        public static IServiceCollection RegisterJobs(this IServiceCollection services, IConfiguration configuration) {

            var workflowOptions = new WorkflowOptions();
            configuration.GetSection(nameof(WorkflowOptions)).Bind(workflowOptions);

            if (workflowOptions.HostedSets.Contains(WorkflowSets.EXAMPLE))
                services.AddSingleton<Job, ExampleJob>();

            return services;

        }

        public static IApplicationBuilder StartJobs(this IApplicationBuilder builder) {

            var jobs = builder.ApplicationServices.GetServices<Job>();

            foreach (var job in jobs) {

                RecurringJob.AddOrUpdate(
                    () => job.StartJob(),
                    job.CronInterval,
                    TimeZoneInfo.Local
                );
            }

            return builder;

        }

    }
}
