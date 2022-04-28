using DomainModel.Models;
using DomainModel.Projections;
using DomainModel.Services.Party;

namespace DomainModel;

public class Party
{
    private readonly CreatePartyService createPartyService;
    private readonly RequestSongService requestSongService;
    private readonly DenySongService denySongService;
    private readonly PlaySongService playSongService;
    private readonly LoadPartyService loadPartyService;
    private readonly UpdatePartyService updatePartyService;

    public Party(CreatePartyService createPartyService,
        RequestSongService requestSongService,
        DenySongService denySongService,
        PlaySongService playSongService,
        LoadPartyService loadPartyService,
        UpdatePartyService updatePartyService)
    {
        this.createPartyService = createPartyService;
        this.requestSongService = requestSongService;
        this.denySongService = denySongService;
        this.playSongService = playSongService;
        this.loadPartyService = loadPartyService;
        this.updatePartyService = updatePartyService;
    }

    public async Task<string> CreateParty(CreatePartyModel model)
    {
        return await createPartyService.CreateParty(model);
    }

    public async Task UpdateParty(UpdatePartyModel model)
    {
        await updatePartyService.UpdateParty(model);
    }

    public async Task RequestSong(RequestSongModel model)
    {
        await requestSongService.RequestSong(model);
    }

    public async Task DenySong(DenySongModel model)
    {
        await denySongService.DenySong(model);
    }

    public async Task PlaySong(PlaySongModel model)
    {
        await playSongService.PlaySong(model);
    }

    public async Task<PartyProjection?> LoadParty(string partyId)
    {
        return await loadPartyService.LoadParty(partyId);
    }
}