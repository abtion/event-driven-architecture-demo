using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DomainModel.DomainEvents;
using DomainModel.Services.Party;

using MediatR;

namespace DomainModel.NotificationHandlers;

internal class UpdatePartyProjectionHandler :
    INotificationHandler<SongRequestedEvent>,
    INotificationHandler<SongPlayedEvent>,
    INotificationHandler<SongDeniedEvent>,
    INotificationHandler<PartyCreatedEvent>,
    INotificationHandler<PartyUpdatedEvent>
{
    private readonly UpdatePartyProjectionService _service;

    public UpdatePartyProjectionHandler(UpdatePartyProjectionService service)
    {
        _service = service;
    }

    public async Task Handle(SongRequestedEvent notification, CancellationToken cancellationToken)
    {
        await _service.AddSongRequestToProjection(notification.Document);
    }

    public async Task Handle(SongPlayedEvent notification, CancellationToken cancellationToken)
    {
        await _service.AddSongPlayedToProjection(notification.Document);
    }

    public async Task Handle(PartyUpdatedEvent notification, CancellationToken cancellationToken)
    {
        await _service.UpdatePartyOnProjection(notification.Document);
    }

    public async Task Handle(PartyCreatedEvent notification, CancellationToken cancellationToken)
    {
        await _service.CreatePartyProjection(notification.Document);
    }

    public async Task Handle(SongDeniedEvent notification, CancellationToken cancellationToken)
    {
        await _service.AddSongDeniedToProjection(notification.Document);
    }
}