namespace DomainModel.Events;

public interface ICosmosDbDocument
{
    public string Id { get; init; }

    public string PartitionKey { get; init; }

    public string Type { get => GetType().Name; }
}