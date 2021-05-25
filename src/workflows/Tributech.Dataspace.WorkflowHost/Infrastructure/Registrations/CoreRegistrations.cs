using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Net.Http;
using Tributech.Dataspace.Workflow.Common.Clients;
using Tributech.Dsk.Api.Clients;
using Tributech.Dsk.Api.Clients.DataApi;
using Tributech.Dsk.Api.Clients.TrustApi;

namespace Tributech.Dataspace.WorkflowHost.Infrastructure.Registrations
{
    public static class CoreRegistrations {

        public static IServiceCollection RegisterCoreComponents(this IServiceCollection services, IConfiguration configuration) {

            var workflowOptions = new WorkflowOptions();
            configuration.GetSection(nameof(WorkflowOptions)).Bind(workflowOptions);

            var dataApiClientOptions = new DataAPIClientOptions();
            configuration.GetSection(nameof(DataAPIClientOptions)).Bind(dataApiClientOptions);

            var trustApiClientOptions = new TrustAPIClientOptions();
            configuration.GetSection(nameof(TrustAPIClientOptions)).Bind(trustApiClientOptions);

            services.AddSingleton(CreateDataAPIClient(dataApiClientOptions));
            services.AddSingleton(CreateTrustAPIClient(trustApiClientOptions));

            if (workflowOptions.HostedSets.Contains(WorkflowSets.EXAMPLE)) {

            }

            return services;
        }

        static DataAPIClient CreateDataAPIClient(DataAPIClientOptions options)
        {
            var authHandler = new APIAuthHandler(
                    options.TokenUrl,
                    options.Scope,
                    options.ClientId,
                    options.ClientSecret);

            var authorizedHttpClient = new HttpClient(authHandler);

            return new DataAPIClient(options.NodeUrl, authorizedHttpClient);
        }

        static TrustAPIClient CreateTrustAPIClient(TrustAPIClientOptions options)
        {
            var authHandler = new APIAuthHandler(
                    options.TokenUrl,
                    options.Scope,
                    options.ClientId,
                    options.ClientSecret);

            var authorizedHttpClient = new HttpClient(authHandler);

            return new TrustAPIClient(options.NodeUrl, authorizedHttpClient);
        }
    }
}
