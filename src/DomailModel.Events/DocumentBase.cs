using Storage.CosmosDb.Abstractions;

namespace DomainModel.Documents;

public record DocumentBase : ICosmosDbDocument
{
    public DocumentBase(string id, string partitionKey, DateTime created)
    {
        Id = id;
        PartitionKey = partitionKey;
        Created = created;
    }

    public string Id { get; init; }

    public string Type => GetType().Name;

    public string PartitionKey { get; init; }

    public DateTime Created { get; set; }
}