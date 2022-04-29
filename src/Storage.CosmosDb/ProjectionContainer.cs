
using DomainModel.Projections;

using Microsoft.Azure.Cosmos;

namespace Storage.CosmosDb;

public class ProjectionContainer : CosmosDbContainerService<ProjectionBase>
{
    public ProjectionContainer(CosmosClient dbClient, string databaseName, string containerName) : base(dbClient, databaseName, containerName)
    {
    }
}