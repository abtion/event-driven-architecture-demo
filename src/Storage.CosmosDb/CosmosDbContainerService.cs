using Microsoft.Azure.Cosmos;

using Storage.CosmosDb.Abstractions;

namespace Storage.CosmosDb;

/// <summary>
/// https://docs.microsoft.com/en-us/azure/cosmos-db/sql/sql-api-dotnet-application
/// </summary>
public class CosmosDbContainerService<T> : ICosmosDbContainerService<T> where T : ICosmosDbItem
{
    private readonly Container _container;

    public CosmosDbContainerService(CosmosClient dbClient, string databaseName, string containerName)
    {
        this._container = dbClient.GetContainer(databaseName, containerName);
    }

    public async Task AddItemAsync(T document)
    {
        await this._container.CreateItemAsync<T>(document, new PartitionKey(document.PartitionKey));
    }

    public async Task DeleteItemAsync(string id, string partitionKey)
    {
        await this._container.DeleteItemAsync<T>(id, new PartitionKey(partitionKey));
    }

    public async Task<T?> GetItemAsync(string id)
    {
        try
        {
            ItemResponse<T> response = await this._container.ReadItemAsync<T>(id, new PartitionKey(id));
            return response.Resource;
        }
        catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return default;
        }
    }

    public async Task<IEnumerable<T>> GetItemsAsync(string queryString)
    {
        var query = this._container.GetItemQueryIterator<T>(new QueryDefinition(queryString));
        List<T> results = new();
        while (query.HasMoreResults)
        {
            var response = await query.ReadNextAsync();

            results.AddRange(response.ToList());
        }

        return results;
    }

    public async Task UpdateItemAsync(string id, T document)
    {
        await this._container.UpsertItemAsync<T>(document, new PartitionKey(document.PartitionKey));
    }
}