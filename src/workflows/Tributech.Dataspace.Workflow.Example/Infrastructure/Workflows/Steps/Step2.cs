using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tributech.Dataspace.Workflow.Example.Core.Entities.DTOs;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace Tributech.Dataspace.Workflow.Example.Infrastructure.Workflows.Steps {
    public class Step2 : IStepBody {

        public StepData Data { get; set; } = new StepData();

        public Step2() {
        }

        public async Task<ExecutionResult> RunAsync(IStepExecutionContext context) {

            Console.WriteLine($"{nameof(Step2)}->{nameof(RunAsync)}");

            Console.WriteLine($"Greeting: {this.Data.Input?.Greeting}");

            return ExecutionResult.Next();

        }
        public class StepData {

            // Input
            public ExampleDTO Input { get; set; }

        }

    }
}
