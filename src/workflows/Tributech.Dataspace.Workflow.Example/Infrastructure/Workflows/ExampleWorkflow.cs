using System;
using System.Collections.Generic;
using System.Text;
using Tributech.Dataspace.Workflow.Common;
using Tributech.Dataspace.Workflow.Example.Core.Entities.DTOs;
using Tributech.Dataspace.Workflow.Example.Infrastructure.Workflows.Steps;
using WorkflowCore.Interface;

namespace Tributech.Dataspace.Workflow.Example.Infrastructure.Workflows {
    public class ExampleWorkflow : Workflow<ExampleWorkflow.WorkflowData> {
        public override string Id => nameof(ExampleWorkflow);

        public override int Version => 1;

        public override void Build(IWorkflowBuilder<WorkflowData> builder) {

            Console.WriteLine($"{nameof(ExampleWorkflow)}->{nameof(Build)}");

            builder
               .StartWith<Step1>()
                    .Output(workflow => workflow.DTO, step => step.Data.Output)
                    .OnError(WorkflowCore.Models.WorkflowErrorHandling.Terminate)
               .Then<Step2>()
                    .Input(step => step.Data.Input, workflow => workflow.DTO)
                    .OnError(WorkflowCore.Models.WorkflowErrorHandling.Terminate);

        }

        public class WorkflowData {

            public ExampleDTO DTO { get; set; }

        }

    }
}
