using DomainModel.Services.Party;
using Microsoft.Extensions.DependencyInjection;

namespace DomainModel;

public static class MusicRequestDomainModelExtensions
{
    public static MusicRequestDomainModelBuilder AddMusicRequestDomainModel(this IServiceCollection services)
    {
        services.AddScoped<MusicRequestDomainModel>();
        services.AddScoped<Party>();
        services.AddScoped<CreatePartyService>();
        services.AddScoped<UpdatePartyService>();
        services.AddScoped<RequestSongService>();
        services.AddScoped<DenySongService>();
        services.AddScoped<PlaySongService>();
        services.AddScoped<LoadPartyService>();

        return new MusicRequestDomainModelBuilder(services);
    }
}