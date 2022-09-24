using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DomainModel.Documents.Party;

using MediatR;

namespace DomainModel.DomainEvents;

internal class SongDeniedEvent : INotification
{
    public SongDeniedEvent(SongDeniedDocument document)
    {
        Document = document;
    }

    public SongDeniedDocument Document { get; }
}