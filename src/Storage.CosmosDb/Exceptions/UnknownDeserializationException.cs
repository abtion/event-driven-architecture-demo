using System.Runtime.Serialization;

namespace Storage.CosmosDb.Exceptions;

[Serializable]
internal class UnknownDeserializationException : Exception
{
    public UnknownDeserializationException()
    {
    }

    public UnknownDeserializationException(string? message) : base(message)
    {
    }

    public UnknownDeserializationException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected UnknownDeserializationException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}