using DomainModel.Events;
using DomainModel.Events.Party;
using DomainModel.Models;

using Storage.CosmosDb;

namespace DomainModel.Services.Party;

public class UpdatePartyService
{
    private readonly ICosmosDbService<EventBase> cosmosDbService;

    public UpdatePartyService(ICosmosDbService<EventBase> cosmosDbService)
    {
        this.cosmosDbService = cosmosDbService;
    }

    internal async Task<string> UpdateParty(UpdatePartyModel model)
    {
        // Event Broker
        var id = Guid.NewGuid().ToString();

        var partyCreated = new PartyUpdated(id, model.partyId, model.Name, DateTime.Now.ToUniversalTime());

        await cosmosDbService.AddItemAsync(partyCreated);

        return id;
    }
}