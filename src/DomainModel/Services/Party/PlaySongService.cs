using DomainModel.Events;
using DomainModel.Events.Party;
using DomainModel.Models;

using Storage.CosmosDb;

namespace DomainModel.Services.Party;

public class PlaySongService
{
    private readonly ICosmosDbService<EventBase> cosmosDbService;

    public PlaySongService(ICosmosDbService<EventBase> cosmosDbService)
    {
        this.cosmosDbService = cosmosDbService;
    }

    internal async Task<string> PlaySong(PlaySongModel model)
    {
        // Event Broker
        var id = Guid.NewGuid().ToString();

        var songRequestMet = new SongRequestMet(id, model.PartyId, model.PartyId, DateTime.Now.ToUniversalTime(), model.SongTitle, model.ArtistName);

        await cosmosDbService.AddItemAsync(songRequestMet);

        return id;
    }
}