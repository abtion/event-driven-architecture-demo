using DomainModel.Documents.Party;
using DomainModel.DomainEvents;
using DomainModel.Models;

using Storage.CosmosDb;

namespace DomainModel.Services.Party;

public class PlaySongService
{
    private readonly ICosmosDbService _cosmosDbService;

    public PlaySongService(ICosmosDbService cosmosDbService)
    {
        this._cosmosDbService = cosmosDbService;
    }

    internal async Task<string> PlaySong(PlaySongModel model)
    {
        // Event Broker
        var id = Guid.NewGuid().ToString();

        var document = new SongPlayedDocument(id, model.PartyId, model.PartyId, DateTime.Now.ToUniversalTime(), model.SongTitle, model.ArtistName);

        await _cosmosDbService.EventContainerService.AddItemAsync(document);
        //await EventBroker.Raise<SongPlayedEvent>(new(document));

        return id;
    }
}