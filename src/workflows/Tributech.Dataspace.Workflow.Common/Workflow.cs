using System;
using WorkflowCore.Interface;

namespace Tributech.Dataspace.Workflow.Common {
    public abstract class Workflow<TData> : IWorkflow<TData> where TData:new() {
        public abstract string Id { get; }
        public abstract int Version { get; }
        public abstract void Build(IWorkflowBuilder<TData> builder);
    }
}
