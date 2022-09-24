namespace DomainModel.Documents.Party;

public record SongPlayedDocument : DocumentBase
{
    public SongPlayedDocument(string id, string partitionKey, string partyId, DateTime created, string songTitle, string artist) : base(id, partitionKey, created)
    {
        SongTitle = songTitle;
        Artist = artist;
        PartyId = partyId;
    }

    public string SongTitle { get; init; }

    public string Artist { get; init; }

    public string PartyId { get; init; }
}