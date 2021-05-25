using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using Tributech.Dataspace.Workflow.Example.Infrastructure.Workflows;
using Tributech.Dataspace.Workflow.Example.Infrastructure.Workflows.Steps;
using WorkflowCore.Interface;

namespace Tributech.Dataspace.WorkflowHost.Infrastructure.Registrations
{
    public static class WorkflowRegistrations {

        public static IServiceCollection RegisterWorkflowSteps(this IServiceCollection services, IConfiguration configuration) {

            var workflowOptions = new WorkflowOptions();
            configuration.GetSection(nameof(WorkflowOptions)).Bind(workflowOptions);

            if (workflowOptions.HostedSets.Contains(WorkflowSets.EXAMPLE)) {

                services.AddTransient<Step1>();
                services.AddTransient<Step2>();

            }

            return services;
        }

        public static IApplicationBuilder StartWorkflowHost(this IApplicationBuilder app, IConfiguration configuration) {

            var workflowOptions = new WorkflowOptions();
            configuration.GetSection(nameof(WorkflowOptions)).Bind(workflowOptions);

            var host = app.ApplicationServices.GetService<IWorkflowHost>();

            if (workflowOptions.HostedSets.Contains(WorkflowSets.EXAMPLE)) {

                host.RegisterWorkflow<ExampleWorkflow, ExampleWorkflow.WorkflowData>();

            }

            host.Start();

            return app;
        }

    }
}
