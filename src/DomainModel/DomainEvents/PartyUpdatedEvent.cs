using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DomainModel.Documents.Party;

using MediatR;

namespace DomainModel.DomainEvents;

internal class PartyUpdatedEvent : INotification
{
    public PartyUpdatedEvent(PartyUpdatedDocument document)
    {
        Document = document;
    }

    public PartyUpdatedDocument Document { get; }
}