#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim as base
LABEL vendor="Tributech.io Solutions"
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["workflows/Tributech.Dataspace.WorkflowHost/Tributech.Dataspace.WorkflowHost.csproj", "workflows/Tributech.Dataspace.WorkflowHost/"]
COPY ["workflows/Tributech.Dataspace.Workflow.Common/Tributech.Dataspace.Workflow.Common.csproj", "workflows/Tributech.Dataspace.Workflow.Common/"]
COPY ["workflows/Tributech.Dataspace.Workflow.Example/Tributech.Dataspace.Workflow.Example.csproj", "workflows/Tributech.Dataspace.Workflow.Example/"]
RUN dotnet restore "workflows/Tributech.Dataspace.WorkflowHost/Tributech.Dataspace.WorkflowHost.csproj"
COPY . .
WORKDIR "/src/workflows/Tributech.Dataspace.WorkflowHost"
RUN dotnet build "Tributech.Dataspace.WorkflowHost.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Tributech.Dataspace.WorkflowHost.csproj" -c Release -o /app/publish

FROM base AS final
EXPOSE 5050
RUN apt-get update \
	&& apt-get install -y --no-install-recommends curl \
	&& rm -rf /var/lib/apt/lists/*
HEALTHCHECK --interval=30s --timeout=30s CMD curl -L --silent --fail --show-error http://localhost:5050/health || exit 1
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Tributech.Dataspace.WorkflowHost.dll"]