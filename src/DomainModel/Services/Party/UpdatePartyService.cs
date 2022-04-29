using DomainModel.Events.Party;
using DomainModel.Models;

using Storage.CosmosDb;

namespace DomainModel.Services.Party;

public class UpdatePartyService
{
    private readonly ICosmosDbService cosmosDbService;

    public UpdatePartyService(ICosmosDbService cosmosDbService)
    {
        this.cosmosDbService = cosmosDbService;
    }

    internal async Task<string> UpdateParty(UpdatePartyModel model)
    {
        // Event Broker
        var id = Guid.NewGuid().ToString();

        var partyCreated = new PartyUpdated(id, model.partyId, model.Name, DateTime.Now.ToUniversalTime());

        await cosmosDbService.EventContainerService.AddItemAsync(partyCreated);

        return id;
    }
}