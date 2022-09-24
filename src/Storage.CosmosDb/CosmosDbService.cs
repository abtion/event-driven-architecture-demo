using DomainModel.Documents;
using DomainModel.Projections;

using Microsoft.Azure.Cosmos;

using Storage.CosmosDb.Abstractions;

namespace Storage.CosmosDb;

/// <summary>
/// https://docs.microsoft.com/en-us/azure/cosmos-db/sql/sql-api-dotnet-application
/// </summary>
public class CosmosDbService : ICosmosDbService
{
    public CosmosDbService(ICosmosDbContainerService<DocumentBase> eventContainerService, ICosmosDbContainerService<ProjectionBase> projectionContainerService)
    {
        EventContainerService = eventContainerService;
        ProjectionContainerService = projectionContainerService;
    }

    public ICosmosDbContainerService<DocumentBase> EventContainerService { get; }
    public ICosmosDbContainerService<ProjectionBase> ProjectionContainerService { get; }
}