using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MediatR;

namespace DomainModel.DomainEvents;

internal static class EventBroker
{
    [ThreadStatic] // ensure separate func per thread to support parallel invocation
    public static Func<IMediator>? Mediator;

    public static async Task Raise<T>(T args) where T : INotification
    {
        if (Mediator == null)
            throw new ArgumentNullException(nameof(Mediator));

        var mediator = Mediator.Invoke();

        await mediator.Publish<T>(args);
    }
}
