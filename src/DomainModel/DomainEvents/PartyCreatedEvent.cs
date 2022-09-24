using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DomainModel.Documents.Party;

using MediatR;

namespace DomainModel.DomainEvents;

internal class PartyCreatedEvent : INotification
{
    public PartyCreatedEvent(PartyCreatedDocument document)
    {
        Document = document;
    }

    public PartyCreatedDocument Document { get; }
}