using DomainModel.Documents;
using DomainModel.Projections;

namespace Storage.CosmosDb;

public interface ICosmosDbService
{
    ICosmosDbContainerService<DocumentBase> EventContainerService { get; }
    ICosmosDbContainerService<ProjectionBase> ProjectionContainerService { get; }
}