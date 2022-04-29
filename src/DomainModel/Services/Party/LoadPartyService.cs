using DomainModel.Events;
using DomainModel.Events.Party;
using DomainModel.Projections;

using Storage.CosmosDb;

namespace DomainModel.Services.Party;

public class LoadPartyService
{
    private readonly ICosmosDbService<EventBase> cosmosDbService;

    public LoadPartyService(ICosmosDbService<EventBase> cosmosDbService)
    {
        this.cosmosDbService = cosmosDbService;
    }

    internal async Task<PartyProjection?> LoadParty(string partyId)
    {
        var events = await cosmosDbService.GetItemsAsync($"SELECT * FROM c WHERE c.partitionKey = \"{partyId}\" ORDER BY c.Created");

        if (events is null)
            return default!;

        PartyProjection result = default!;

        foreach (var myevent in events)
        {
            var partyCreated = myevent as PartyCreated;
            var partyUpdated = myevent as PartyUpdated;
            var songRequested = myevent as SongRequested;
            var songRequestMet = myevent as SongRequestMet;
            var songRequestDenied = myevent as SongRequestDenied;

            if (partyCreated != null)
            {
                result = new PartyProjection(partyCreated.Id, partyCreated.Name, new(), new(), new());
            }

            if (partyUpdated != null)
            {
                result.Name = partyUpdated.Name;
            }

            if (songRequested != null)
            {
                result.RequestedSongs.Add(new SongRequestProjection(songRequested.SongTitle, songRequested.Artist, songRequested.Created));
            }

            if (songRequestMet != null)
            {
                result.RequestedSongs.RemoveAll(p => p.SongTitle == songRequestMet.SongTitle && p.ArtistName == songRequestMet.Artist);
                result.PlayedSongs.Add(new PlayedSongProjection(songRequestMet.SongTitle, songRequestMet.Artist, songRequestMet.Created));
            }

            if (songRequestDenied != null)
            {
                result.RequestedSongs.RemoveAll(p => p.SongTitle == songRequestDenied.SongTitle && p.ArtistName == songRequestDenied.Artist);
                result.DeniedSongs.Add(new DeniedSongsProjection(songRequestDenied.SongTitle, songRequestDenied.Artist, songRequestDenied.ReasonForNotPlayingSong));
            }
        }

        return result;
    }
}