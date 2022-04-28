using Newtonsoft.Json;

namespace DomainModel.Events;

public abstract record EventBase : ICosmosDbDocument
{
    public EventBase(string id, string partitionKey, DateTime created)
    {
        Id = id;
        PartitionKey = partitionKey;
        Created = created;
    }

    [JsonProperty("id")] public string Id { get; init; }

    [JsonProperty("partitionKey")] public string PartitionKey { get; init; }

    [JsonProperty("$type")]
    public abstract string Type { get; }

    public DateTime Created { get; set; }
}