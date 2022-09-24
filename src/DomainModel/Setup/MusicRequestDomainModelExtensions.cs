using DomainModel.Services.Party;

using Microsoft.Extensions.DependencyInjection;

namespace DomainModel.Setup;

public static class MusicRequestDomainModelExtensions
{
    public static MusicRequestDomainModelBuilder AddMusicRequestDomainModel(this IServiceCollection services)
    {
        services
            .AddScoped<MusicRequestDomainModel>()
            .AddScoped<Party>()
            .AddScoped<CreatePartyService>()
            .AddScoped<UpdatePartyService>()
            .AddScoped<RequestSongService>()
            .AddScoped<DenySongService>()
            .AddScoped<PlaySongService>()
            .AddScoped<LoadPartyProjectionService>()
            .AddScoped<ValidateInputModel>()
            .AddScoped<RequestSongService>()
            .AddScoped<DenySongService>();

        return new MusicRequestDomainModelBuilder(services);
    }
}