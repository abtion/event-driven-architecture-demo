using MediatR;

namespace DomainModel.EventHandlers;

public class GenericRequest<T> : IRequest<GenericResponse>
{
    public GenericRequest(T value)
    {
        Value = value;
    }

    public T Value { get; }
}