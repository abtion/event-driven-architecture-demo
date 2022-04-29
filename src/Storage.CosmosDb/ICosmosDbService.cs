using DomainModel.Events;
using DomainModel.Projections;

namespace Storage.CosmosDb;

public interface ICosmosDbService
{
    ICosmosDbContainerService<EventBase> EventContainerService { get; }
    ICosmosDbContainerService<ProjectionBase> ProjectionContainerService { get; }
}