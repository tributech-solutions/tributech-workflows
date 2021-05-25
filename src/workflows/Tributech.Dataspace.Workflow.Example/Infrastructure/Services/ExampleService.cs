using System;
using System.Threading;
using System.Threading.Tasks;
using Tributech.Dataspace.Workflow.Common;

namespace Tributech.Dataspace.Workflow.Example.Infrastructure.Services
{
    public class ExampleService : Service {

        public ExampleService() {
        }

        public override Task StartAsync(CancellationToken cancellationToken) {
            return base.StartAsync(cancellationToken);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken) {

            Console.WriteLine($"{nameof(ExampleService)}->{nameof(ExecuteAsync)}");

            return Task.Delay(10);

        }

        public override Task StopAsync(CancellationToken cancellationToken) {
            return base.StopAsync(cancellationToken);
        }

    }

}
