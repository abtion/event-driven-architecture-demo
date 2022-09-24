using DomainModel.Documents.Party;
using DomainModel.Projections;

using Storage.CosmosDb;

namespace DomainModel.Services.Party;

public class LoadPartyProjectionService
{
    private readonly ICosmosDbService _cosmosDbService;

    public LoadPartyProjectionService(ICosmosDbService cosmosDbService)
    {
        this._cosmosDbService = cosmosDbService;
    }

    internal async Task<PartyProjection?> LoadParty(string partyId)
    {
        var projection = await _cosmosDbService.ProjectionContainerService.GetItemAsync(partyId);

        return (PartyProjection)projection;
    }
}