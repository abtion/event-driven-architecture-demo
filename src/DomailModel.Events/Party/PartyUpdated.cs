namespace DomainModel.Events.Party;

public record PartyUpdated : EventBase
{
    public PartyUpdated(string id, string partitionKey, string name, DateTime created) : base(id, partitionKey, created)
    {
        Name = name;
    }

    public string Name { get; init; }
}