using DomainModel.Documents.Party;
using DomainModel.DomainEvents;
using DomainModel.Models;
using Storage.CosmosDb;

namespace DomainModel.Services.Party;

public class RequestSongService
{
    private readonly ICosmosDbService _cosmosDbService;

    public RequestSongService(ICosmosDbService cosmosDbService)
    {
        this._cosmosDbService = cosmosDbService;
    }

    internal async Task<string> RequestSong(RequestSongModel model)
    {
        var id = Guid.NewGuid().ToString();

        var document = new SongRequestedDocument(id, model.PartyId, model.PartyId, DateTime.Now.ToUniversalTime(), model.SongTitle, model.ArtistName);

        await _cosmosDbService.EventContainerService.AddItemAsync(document);
        //await EventBroker.Raise<SongRequestedEvent>(new SongRequestedEvent(document));

        return id;
    }
}