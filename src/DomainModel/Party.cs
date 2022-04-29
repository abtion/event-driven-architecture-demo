using DomainModel.Models;
using DomainModel.Projections;
using DomainModel.Services.Party;

namespace DomainModel;

public class Party
{
    private readonly CreatePartyService _createPartyService;
    private readonly RequestSongService _requestSongService;
    private readonly DenySongService _denySongService;
    private readonly PlaySongService _playSongService;
    private readonly LoadPartyService _loadPartyService;
    private readonly UpdatePartyService _updatePartyService;

    public Party(CreatePartyService createPartyService,
        RequestSongService requestSongService,
        DenySongService denySongService,
        PlaySongService playSongService,
        LoadPartyService loadPartyService,
        UpdatePartyService updatePartyService)
    {
        _createPartyService = createPartyService;
        _requestSongService = requestSongService;
        _denySongService = denySongService;
        _playSongService = playSongService;
        _loadPartyService = loadPartyService;
        _updatePartyService = updatePartyService;
    }

    public async Task<string> CreateParty(CreatePartyModel model)
    {
        return await _createPartyService.CreateParty(model);
    }

    public async Task UpdateParty(UpdatePartyModel model)
    {
        await _updatePartyService.UpdateParty(model);
    }

    public async Task RequestSong(RequestSongModel model)
    {
        await _requestSongService.RequestSong(model);
    }

    public async Task DenySong(DenySongModel model)
    {
        await _denySongService.DenySong(model);
    }

    public async Task PlaySong(PlaySongModel model)
    {
        await _playSongService.PlaySong(model);
    }

    public async Task<PartyProjection?> LoadParty(string partyId)
    {
        return await _loadPartyService.LoadParty(partyId);
    }
}