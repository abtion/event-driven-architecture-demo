
using Newtonsoft.Json;

namespace Storage.CosmosDb.Abstractions;

public interface ICosmosDbDocument
{
    [JsonProperty("id")] string Id { get; init; }

    [JsonProperty("partitionKey")] string PartitionKey { get; init; }

    [JsonProperty("$type")] string Type { get; }
}