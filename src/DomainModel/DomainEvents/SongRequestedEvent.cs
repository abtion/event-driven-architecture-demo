using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DomainModel.Documents.Party;

using MediatR;

namespace DomainModel.DomainEvents;

public class SongRequestedEvent : INotification
{
    public SongRequestedEvent(SongRequestedDocument document)
    {
        Document = document;
    }

    public SongRequestedDocument Document { get; }
}