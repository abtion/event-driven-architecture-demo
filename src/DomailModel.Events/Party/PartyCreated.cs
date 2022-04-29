namespace DomainModel.Events.Party;

public record PartyCreated : EventBase
{
    public PartyCreated(string id, string partitionKey, string name, DateTime created) : base(id, partitionKey, created)
    {
        Name = name;
    }

    public string Name { get; init; }
}