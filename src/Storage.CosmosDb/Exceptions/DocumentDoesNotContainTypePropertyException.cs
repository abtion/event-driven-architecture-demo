using System.Runtime.Serialization;

namespace Storage.CosmosDb.Exceptions;

[Serializable]
internal class DocumentDoesNotContainTypePropertyException : Exception
{
    public DocumentDoesNotContainTypePropertyException()
    {
    }

    public DocumentDoesNotContainTypePropertyException(string? message) : base(message)
    {
    }

    public DocumentDoesNotContainTypePropertyException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected DocumentDoesNotContainTypePropertyException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}