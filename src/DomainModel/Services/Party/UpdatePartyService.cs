using DomainModel.Documents.Party;
using DomainModel.DomainEvents;
using DomainModel.Models;

using Storage.CosmosDb;

namespace DomainModel.Services.Party;

public class UpdatePartyService
{
    private readonly ICosmosDbService _cosmosDbService;

    public UpdatePartyService(ICosmosDbService cosmosDbService)
    {
        this._cosmosDbService = cosmosDbService;
    }

    internal async Task<string> UpdateParty(UpdatePartyModel model)
    {
        var id = Guid.NewGuid().ToString();

        var document = new PartyUpdatedDocument(id, model.partyId, model.Name, DateTime.Now.ToUniversalTime());

        await _cosmosDbService.EventContainerService.AddItemAsync(document);
        //await EventBroker.Raise<PartyUpdatedEvent>(new(document));

        return id;
    }
}