using DomainModel.Events.Party;
using DomainModel.Models;

using Storage.CosmosDb;

namespace DomainModel.Services.Party;

public class CreatePartyService
{
    private readonly ICosmosDbService cosmosDbService;

    public CreatePartyService(ICosmosDbService cosmosDbService)
    {
        this.cosmosDbService = cosmosDbService;
    }

    internal async Task<string> CreateParty(CreatePartyModel model)
    {
        // Event Broker
        var id = Guid.NewGuid().ToString();

        var partyCreated = new PartyCreated(id, id, model.Name, DateTime.Now.ToUniversalTime());

        await cosmosDbService.EventContainerService.AddItemAsync(partyCreated);

        return id;
    }
}