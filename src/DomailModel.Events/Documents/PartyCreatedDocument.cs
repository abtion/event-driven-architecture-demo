namespace DomainModel.Documents.Party;

public record PartyCreatedDocument : DocumentBase
{
    public PartyCreatedDocument(string id, string partitionKey, string name, DateTime created) : base(id, partitionKey, created)
    {
        Name = name;
    }

    public string Name { get; init; }
}