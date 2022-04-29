using DomainModel.Events;
using DomainModel.Projections;

using Microsoft.Azure.Cosmos;

using Storage.CosmosDb.Abstractions;

namespace Storage.CosmosDb;

/// <summary>
/// https://docs.microsoft.com/en-us/azure/cosmos-db/sql/sql-api-dotnet-application
/// </summary>
public class CosmosDbService : ICosmosDbService
{
    public CosmosDbService(ICosmosDbContainerService<EventBase> eventContainerService, ICosmosDbContainerService<ProjectionBase> projectionContainerService)
    {
        EventContainerService = eventContainerService;
        ProjectionContainerService = projectionContainerService;
    }

    public ICosmosDbContainerService<EventBase> EventContainerService { get; }
    public ICosmosDbContainerService<ProjectionBase> ProjectionContainerService { get; }
}