namespace DomainModel.Projections;

public record PartyProjection
{
    public PartyProjection(
        string id,
        string name,
        List<PlayedSongProjection> playedSongs,
        List<DeniedSongsProjection> deniedSongs,
        List<SongRequestProjection> requestedSongs)
    {
        Id = id;
        Name = name;
        PlayedSongs = playedSongs;
        DeniedSongs = deniedSongs;
        RequestedSongs = requestedSongs;
    }

    public string Id { get; init; }
    public string Name { get; set; }
    public List<PlayedSongProjection> PlayedSongs { get; init; }
    public List<DeniedSongsProjection> DeniedSongs { get; init; }
    public List<SongRequestProjection> RequestedSongs { get; init; }
}

public record SongRequestProjection(
    string SongTitle,
    string ArtistName,
    DateTime RequestedAt);

public record PlayedSongProjection(
    string SongTitle,
    string ArtistName,
    DateTime PlayedAt);

public record DeniedSongsProjection(
    string ArtistName,
    string SongTitle,
    string ReasonForNotPlayingSong);
