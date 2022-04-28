using DomainModel.Events;
using MediatR;

namespace DomainModel.EventHandlers;

public class SongAddedToPlaylist : IRequestHandler<GenericRequest<SongRequested>, GenericResponse>
{
    public async Task<GenericResponse> Handle(GenericRequest<SongRequested> request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        throw new NotImplementedException();
    }
}