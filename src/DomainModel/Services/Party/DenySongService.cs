using DomainModel.Documents.Party;
using DomainModel.Models;

using Storage.CosmosDb;

namespace DomainModel.Services.Party;

public class DenySongService
{
    private readonly ICosmosDbService _cosmosDbService;

    public DenySongService(ICosmosDbService cosmosDbService)
    {
        this._cosmosDbService = cosmosDbService;
    }

    internal async Task<string> DenySong(DenySongModel model)
    {
        // Event Broker
        var id = Guid.NewGuid().ToString();

        var songDenied = new SongDeniedDocument(id, model.PartyId, model.PartyId, DateTime.Now.ToUniversalTime(), model.SongTitle, model.ArtistName, model.ReasonForNotPlayingSong);

        await _cosmosDbService.EventContainerService.AddItemAsync(songDenied);

        return id;
    }
}