using DomainModel.Documents.Party;
using DomainModel.Models;

using Storage.CosmosDb;

namespace DomainModel.Services.Party;

public class CreatePartyService
{
    private readonly ICosmosDbService _cosmosDbService;
    private readonly ValidateInputModel _validateInputModel;

    public CreatePartyService(ICosmosDbService cosmosDbService, ValidateInputModel validateInputModel)
    {
        this._cosmosDbService = cosmosDbService;
        _validateInputModel = validateInputModel;
    }

    internal async Task<string> CreateParty(CreatePartyModel model)
    {
        _validateInputModel.Validate(model);

        // Event Broker
        var id = Guid.NewGuid().ToString();

        var partyCreated = new PartyCreatedDocument(id, id, model.Name, DateTime.Now.ToUniversalTime());

        await _cosmosDbService.EventContainerService.AddItemAsync(partyCreated);

        return id;
    }
}