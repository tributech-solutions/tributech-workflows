using System;
using Tributech.Dataspace.Workflow.Common;
using Tributech.Dataspace.Workflow.Example.Infrastructure.Workflows;
using WorkflowCore.Interface;

namespace Tributech.Dataspace.Workflow.Example.Infrastructure.Jobs
{
    public class ExampleJob : MinutelyJob
    {
        public IWorkflowHost _workflowHost;

        public ExampleJob(IWorkflowHost workflowHost) {
            _workflowHost = workflowHost;
        }

        public override void StartJob() {

            Console.WriteLine($"{nameof(ExampleJob)}->{nameof(StartJob)}");

            _workflowHost.StartWorkflow(nameof(ExampleWorkflow)).Wait();

        }
    }
}
