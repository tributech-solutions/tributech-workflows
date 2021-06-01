# Tributech Workflows

Tributech Workflows contains executions pipelines for different use cases.

## Setting up with docker

After deploying the service with docker compose you can get to the UI with http://localhost:5050/hangfire

# Developement
As a general guide line - use the "Tributech.Dataspace.Workflow.Example" project to get a starting point.

## Create a new project
All workflows are currently organized as their own project and we strongly suggest to create a new project for your workflow as well.

## Scheduling types
### Job
**Tributech.Dataspace.Workflow.Common.Job**

A Job is executed by the framework with a set interval. Overwrite the **CronInterval** property to define a schedule. https://en.wikipedia.org/wiki/Cron

### Service
**Tributech.Dataspace.Workflow.Common.Service**

A service is started once and will then run until the service, or the server, stops.

### Workflow
**Tributech.Dataspace.Workflow.Common.Workflow**

Workflows offer to split up the workload into multiple steps. The input/output of these steps is held in a database and this makes it so a workflow can continue where it left off in case of an interruption.

Call your workflows from either a job or service, depending on what timing method fits the use case best.

## Business Logic
Once you found a scheduling type that fits your requirements, your business logic will end up either in steps or directly in a job/service, if the granularity of a workflow is not required.

## Wiring up the workflow
First, add an entry to the "WorkflowSets" class. Its data is generated from the appsettings.json file and will be used to decide, which services/jobs/workflows will be setup.

Head to **Tributech.Dataspace.WorkflowHost/Infrastructure/Registrations** to see all the registrations and plug your classes where necessary. Please note, that a workflow will not work, unless all its steps are registered too!

Custom configurations should be registered in the **CoreRegistration**.