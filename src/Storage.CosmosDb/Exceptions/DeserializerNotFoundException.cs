using System.Runtime.Serialization;

namespace Storage.CosmosDb.Exceptions;

[Serializable]
internal class DeserializerNotFoundException : Exception
{
    public DeserializerNotFoundException()
    {
    }

    public DeserializerNotFoundException(string? message) : base(message)
    {
    }

    public DeserializerNotFoundException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected DeserializerNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}