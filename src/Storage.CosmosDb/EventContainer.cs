
using DomainModel.Events;

using Microsoft.Azure.Cosmos;

namespace Storage.CosmosDb;

public class EventContainer : CosmosDbContainerService<EventBase>
{
    public EventContainer(CosmosClient dbClient, string databaseName, string containerName) : base(dbClient, databaseName, containerName)
    {
    }
}