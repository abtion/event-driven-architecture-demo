namespace DomainModel.Documents.Party;

public record PartyUpdatedDocument : DocumentBase
{
    public PartyUpdatedDocument(string id, string partitionKey, string name, DateTime created) : base(id, partitionKey, created)
    {
        Name = name;
    }

    public string Name { get; init; }
}