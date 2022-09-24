using DomainModel.Documents;
using DomainModel.Documents.Party;
using DomainModel.Projections;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using Storage.CosmosDb.Abstractions;
using Storage.CosmosDb.Exceptions;

namespace Storage.CosmosDb;

public class EventJsonConverter : JsonConverter
{
    // This converter handles only deserialization, not serialization.
    public override bool CanRead => true;
    public override bool CanWrite => false;

    public override bool CanConvert(Type objectType)
    {
        // Only if the target type is the abstract base class
        return objectType == typeof(DocumentBase);
    }

    public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
    {
        // First, just read the JSON as a JObject
        var obj = JObject.Load(reader);

        if (obj is null)
            throw new NotPossibleToLoadObjectToDeserializeException();

        // Then look at the $type property:
        var typeName = obj["$type"]?.Value<string>();

        if (typeName is null)
            throw new DocumentDoesNotContainTypePropertyException();

        ICosmosDbDocument? _item = null;

        // maybe use reflection to initialize the instances here?
        switch (typeName)
        {
            case nameof(PartyCreatedDocument):
                _item = obj.ToObject<PartyCreatedDocument>(serializer);
                break;

            case nameof(PartyUpdatedDocument):
                _item = obj.ToObject<PartyUpdatedDocument>(serializer);
                break;

            case nameof(SongDeniedDocument):
                _item = obj.ToObject<SongDeniedDocument>(serializer);
                break;

            case nameof(SongRequestedDocument):
                _item = obj.ToObject<SongRequestedDocument>(serializer);
                break;

            case nameof(SongPlayedDocument):
                _item = obj.ToObject<SongPlayedDocument>(serializer);
                break;

            case nameof(PartyProjection):
                _item = obj.ToObject<PartyProjection>(serializer);
                break;
        }

        if (_item is null)
            throw new DeserializerNotFoundException($"Deserialization of object of type '{typeName}' not found");

        return _item;
    }

    public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
    {
        throw new NotSupportedException("This converter handles only deserialization, not serialization.");
    }
}