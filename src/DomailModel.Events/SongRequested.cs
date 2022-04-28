namespace DomainModel.Events;

public record SongRequested : EventBase
{
    public SongRequested(string id, string partitionKey, string partyId, DateTime created, string songTitle, string artist) : base(id, partitionKey, created)
    {
        Id = id;
        PartitionKey = partitionKey;
        Created = created;
        SongTitle = songTitle;
        Artist = artist;
        PartyId = partyId;
    }

    public string SongTitle { get; init; }

    public string Artist { get; init; }

    public string PartyId { get; init; }

    public override string Type => "songRequested";
}
