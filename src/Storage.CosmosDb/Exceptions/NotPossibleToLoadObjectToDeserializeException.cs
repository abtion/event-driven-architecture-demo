using System.Runtime.Serialization;

namespace Storage.CosmosDb.Exceptions;

[Serializable]
internal class NotPossibleToLoadObjectToDeserializeException : Exception
{
    public NotPossibleToLoadObjectToDeserializeException()
    {
    }

    public NotPossibleToLoadObjectToDeserializeException(string? message) : base(message)
    {
    }

    public NotPossibleToLoadObjectToDeserializeException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected NotPossibleToLoadObjectToDeserializeException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}