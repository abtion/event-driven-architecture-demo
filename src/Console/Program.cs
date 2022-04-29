using DomainModel;
using DomainModel.Models;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

#region Configuration stuff
var settings = new Dictionary<string, string> {
        { $"MusicRequest:DomainModel:CosmosDb:Account", "https://localhost:8081" },
        { $"MusicRequest:DomainModel:CosmosDb:Key", "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==" },
        { $"MusicRequest:DomainModel:CosmosDb:DatabaseName", "MusicRequest" },
        { $"MusicRequest:DomainModel:CosmosDb:EventsContainerName", "Events" },
        { $"MusicRequest:DomainModel:CosmosDb:ProjectionsContainerName", "Projections" },
};

var configBuilder = new ConfigurationBuilder();
var confSection = configBuilder.AddInMemoryCollection(settings)
    .Build()
    .GetSection("MusicRequest:DomainModel:CosmosDb");
#endregion Configuration stuff

#region Dependency Injection stuff
// https://www.programmingwithwolfgang.com/configure-dependency-injection-for-net-5-console-applications/
// https://docs.microsoft.com/en-us/dotnet/core/extensions/dependency-injection-usage
var hostBuilder = Host.CreateDefaultBuilder(args);
hostBuilder.ConfigureServices((_, services) =>
{
    services.AddMusicRequestDomainModel()
        .UseCosmosDb(confSection);
});

var host = hostBuilder.Build();
#endregion Dependency Injection stuff

// Event Producer
var domainModel = host.Services.GetService<MusicRequestDomainModel>();

if (domainModel is null)
    throw new Exception("MusicRequestDomainModel is not in the service collection");

var partyId = await domainModel.Party.CreateParty(new CreatePartyModel($"This is my party"));

await domainModel.Party.RequestSong(new RequestSongModel(partyId, "Taylor Swift", "Blank Space"));
await domainModel.Party.RequestSong(new RequestSongModel(partyId, "Ed Sheeran", "Bad Habits"));
await domainModel.Party.RequestSong(new RequestSongModel(partyId, "Harry Styles", "As it was"));
await domainModel.Party.RequestSong(new RequestSongModel(partyId, "Dean Lewis", "Half a Man"));
await domainModel.Party.RequestSong(new RequestSongModel(partyId, "Dean Lewis", "Be Alright"));
await domainModel.Party.PlaySong(new PlaySongModel(partyId, "Taylor Swift", "Blank Space"));
await domainModel.Party.RequestSong(new RequestSongModel(partyId, "Ed Sheeran", "Bad Habits"));
await domainModel.Party.UpdateParty(new UpdatePartyModel(partyId, "This is our party"));
await domainModel.Party.DenySong(new DenySongModel(partyId, "Dean Lewis", "Be Alright", "This song is way too slow for the dancefloor"));
await domainModel.Party.PlaySong(new PlaySongModel(partyId, "Ed Sheeran", "Bad Habits"));

await PrintParty();

Console.WriteLine("starting host");

await host.RunAsync();

#region "Private Methods"
async Task PrintParty()
{
    var party = await domainModel!.Party.LoadParty(partyId!);

    if (party is null)
        return;

    Console.WriteLine($"*** Party: {party.Name} ***");
    Console.WriteLine($"*** Requested Songs ({party.RequestedSongs.Count}) ***");

    foreach (var song in party.RequestedSongs)
        Console.WriteLine($"{song.ArtistName}: {song.SongTitle} ({song.RequestedAt.ToShortTimeString()})");

    Console.WriteLine($"*** Played Songs ({party.PlayedSongs.Count}) ***");

    foreach (var song in party.PlayedSongs)
        Console.WriteLine($"{song.ArtistName}: {song.SongTitle} ({song.PlayedAt.ToShortTimeString()})");

    Console.WriteLine($"*** Denied Songs ({party.DeniedSongs.Count}) ***");

    foreach (var song in party.DeniedSongs)
        Console.WriteLine($"{song.ArtistName}: {song.SongTitle} - {song.ReasonForNotPlayingSong})");
}
#endregion