using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using Tributech.Dataspace.Workflow.Example.Infrastructure.Services;

namespace Tributech.Dataspace.WorkflowHost.Infrastructure.Registrations
{
    public static class ServiceRegistrations
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            var workflowOptions = new WorkflowOptions();
            configuration.GetSection(nameof(WorkflowOptions)).Bind(workflowOptions);

            if (workflowOptions.HostedSets.Contains(WorkflowSets.EXAMPLE))
                services.AddHostedService<ExampleService>();

            return services;        
        }
    }
}
