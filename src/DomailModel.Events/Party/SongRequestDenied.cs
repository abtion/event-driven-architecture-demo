namespace DomainModel.Events.Party;

public record SongRequestDenied : EventBase
{
    public SongRequestDenied(string id, string partitionKey, string partyId, DateTime created, string songTitle, string artist, string reasonForNotPlayingSong) : base(id, partitionKey, created)
    {
        SongTitle = songTitle;
        Artist = artist;
        ReasonForNotPlayingSong = reasonForNotPlayingSong;
        PartyId = partyId;
    }

    public string SongTitle { get; init; }

    public string Artist { get; init; }

    public string ReasonForNotPlayingSong { get; init; }

    public string PartyId { get; init; }
}