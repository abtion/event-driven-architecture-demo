using DomainModel.Events;
using DomainModel.Models;
using Storage.CosmosDb;

namespace DomainModel.Services.Party;

public class DenySongService
{
    private readonly ICosmosDbService cosmosDbService;

    public DenySongService(ICosmosDbService cosmosDbService)
    {
        this.cosmosDbService = cosmosDbService;
    }

    internal async Task<string> DenySong(DenySongModel model)
    {
        // Event Broker
        var id = Guid.NewGuid().ToString();

        var songDenied = new SongRequestDenied(id, model.PartyId, model.PartyId, DateTime.Now.ToUniversalTime(), model.SongTitle, model.ArtistName, model.ReasonForNotPlayingSong);

        await cosmosDbService.AddItemAsync(songDenied);

        return id;
    }
}