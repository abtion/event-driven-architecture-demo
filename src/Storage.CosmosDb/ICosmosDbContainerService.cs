namespace Storage.CosmosDb;

public interface ICosmosDbContainerService<T>
{
    Task<IEnumerable<T>> GetItemsAsync(string query);
    Task<T?> GetItemAsync(string id);
    Task AddItemAsync(T item);
    Task UpdateItemAsync(string id, T item);
    Task DeleteItemAsync(string id, string partitionKey);
}