using DomainModel.Events.Party;
using DomainModel.Models;

using Storage.CosmosDb;

namespace DomainModel.Services.Party;

public class PlaySongService
{
    private readonly ICosmosDbService cosmosDbService;

    public PlaySongService(ICosmosDbService cosmosDbService)
    {
        this.cosmosDbService = cosmosDbService;
    }

    internal async Task<string> PlaySong(PlaySongModel model)
    {
        // Event Broker
        var id = Guid.NewGuid().ToString();

        var songRequestMet = new SongRequestMet(id, model.PartyId, model.PartyId, DateTime.Now.ToUniversalTime(), model.SongTitle, model.ArtistName);

        await cosmosDbService.EventContainerService.AddItemAsync(songRequestMet);

        return id;
    }
}