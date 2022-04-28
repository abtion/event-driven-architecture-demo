using DomainModel.Events;
using DomainModel.Models;
using Storage.CosmosDb;

namespace DomainModel.Services.Party;

public class RequestSongService
{
    private readonly ICosmosDbService cosmosDbService;

    public RequestSongService(ICosmosDbService cosmosDbService)
    {
        this.cosmosDbService = cosmosDbService;
    }

    internal async Task<string> RequestSong(RequestSongModel model)
    {
        // Event Broker
        var id = Guid.NewGuid().ToString();

        var songRequested = new SongRequested(id, model.PartyId, model.PartyId, DateTime.Now.ToUniversalTime(), model.SongTitle, model.ArtistName);

        await cosmosDbService.AddItemAsync(songRequested);

        return id;
    }
}