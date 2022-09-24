
using DomainModel.Documents;

using Microsoft.Azure.Cosmos;

namespace Storage.CosmosDb;

public class EventContainer : CosmosDbContainerService<DocumentBase>
{
    public EventContainer(CosmosClient dbClient, string databaseName, string containerName) : base(dbClient, databaseName, containerName)
    {
    }
}