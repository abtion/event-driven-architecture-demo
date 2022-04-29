using DomainModel.Events;
using DomainModel.Events.Party;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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
        return objectType == typeof(EventBase);
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

        EventBase? _event = null;

        switch (typeName)
        {
            case nameof(PartyCreated):
                _event = obj.ToObject<PartyCreated>(serializer);
                break;

            case nameof(PartyUpdated):
                _event = obj.ToObject<PartyUpdated>(serializer);
                break;

            case nameof(SongRequestDenied):
                _event = obj.ToObject<SongRequestDenied>(serializer);
                break;

            case nameof(SongRequested):
                _event = obj.ToObject<SongRequested>(serializer);
                break;

            case nameof(SongRequestMet):
                _event = obj.ToObject<SongRequestMet>(serializer);
                break;
        }

        if (_event is null)
            throw new DeserializerNotFoundException($"Deserialization of object of type '{typeName}' not found");

        return _event;
    }

    public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
    {
        throw new NotSupportedException("This converter handles only deserialization, not serialization.");
    }
}