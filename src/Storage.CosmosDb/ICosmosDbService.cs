using DomainModel.Events;

namespace Storage.CosmosDb;

public interface ICosmosDbService
{
    Task<IEnumerable<EventBase>> GetItemsAsync(string query);
    Task<EventBase?> GetItemAsync(string id);
    Task AddItemAsync(EventBase item);
    Task UpdateItemAsync(string id, EventBase item);
    Task DeleteItemAsync(string id, string partitionKey);
}