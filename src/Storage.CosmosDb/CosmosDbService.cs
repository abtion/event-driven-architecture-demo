using DomainModel.Events;
using Microsoft.Azure.Cosmos;

namespace Storage.CosmosDb;

/// <summary>
/// https://docs.microsoft.com/en-us/azure/cosmos-db/sql/sql-api-dotnet-application
/// </summary>
public class CosmosDbService : ICosmosDbService
{
    private readonly Container _container;

    public CosmosDbService(CosmosClient dbClient, string databaseName, string containerName)
    {
        this._container = dbClient.GetContainer(databaseName, containerName);
    }

    public async Task AddItemAsync(EventBase document)
    {
        await this._container.CreateItemAsync<EventBase>(document, new PartitionKey(document.PartitionKey));
    }

    public async Task DeleteItemAsync(string id, string partitionKey)
    {
        await this._container.DeleteItemAsync<EventBase>(id, new PartitionKey(partitionKey));
    }

    public async Task<EventBase?> GetItemAsync(string id)
    {
        try
        {
            ItemResponse<EventBase> response = await this._container.ReadItemAsync<EventBase>(id, new PartitionKey(id));
            return response.Resource;
        }
        catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return null;
        }
    }

    public async Task<IEnumerable<EventBase>> GetItemsAsync(string queryString)
    {
        var query = this._container.GetItemQueryIterator<EventBase>(new QueryDefinition(queryString));
        List<EventBase> results = new();
        while (query.HasMoreResults)
        {
            var response = await query.ReadNextAsync();

            results.AddRange(response.ToList());
        }

        return results;
    }

    public async Task UpdateItemAsync(string id, EventBase document)
    {
        await this._container.UpsertItemAsync<EventBase>(document, new PartitionKey(document.PartitionKey));
    }
}