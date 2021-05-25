using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tributech.Dataspace.Workflow.Example.Core.Entities.DTOs;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace Tributech.Dataspace.Workflow.Example.Infrastructure.Workflows.Steps {
    public class Step1 : IStepBody {

        public StepData Data { get; set; } = new StepData();

        public Step1() {
        }

        public async Task<ExecutionResult> RunAsync(IStepExecutionContext context) {

            Console.WriteLine($"{nameof(Step1)}->{nameof(RunAsync)}");

            this.Data.Output = new ExampleDTO() { Greeting = "Hello Workflow!"};

            return ExecutionResult.Next();

        }
        public class StepData {

            // Output
            public ExampleDTO Output { get; set; }

        }

    }
}
