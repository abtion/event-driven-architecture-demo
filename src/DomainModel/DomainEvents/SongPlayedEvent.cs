using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DomainModel.Documents.Party;

using MediatR;

namespace DomainModel.DomainEvents;

internal class SongPlayedEvent : INotification
{
    public SongPlayedEvent(SongPlayedDocument document)
    {
        Document = document;
    }

    public SongPlayedDocument Document { get; }
}