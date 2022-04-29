using System.Text;

using Microsoft.Azure.Cosmos;

using Newtonsoft.Json;

namespace Storage.CosmosDb;

/// <summary>
/// https://thomaslevesque.com/2019/10/14/handling-type-hierarchies-in-cosmos-db-part1/
/// https://thomaslevesque.com/2019/10/15/handling-type-hierarchies-in-cosmos-db-part-2/
/// </summary>
public class NewtonsoftJsonCosmosSerializer : CosmosSerializer
{
    private static readonly Encoding DefaultEncoding = new UTF8Encoding(false, true);

    private readonly JsonSerializer _serializer;

    public NewtonsoftJsonCosmosSerializer(JsonSerializerSettings settings)
    {
        _serializer = JsonSerializer.Create(settings);
    }

    public override T FromStream<T>(Stream stream)
    {
        if (typeof(Stream).IsAssignableFrom(typeof(T)))
        {
            return (T)(object)stream;
        }

        using var sr = new StreamReader(stream);
        using var jsonTextReader = new JsonTextReader(sr);

        var result = _serializer.Deserialize<T>(jsonTextReader);

        if (result is null)
            return default!;

        return result;
    }

    public override Stream ToStream<T>(T input)
    {
        var streamPayload = new MemoryStream();
        using (var streamWriter = new StreamWriter(streamPayload, encoding: DefaultEncoding, bufferSize: 1024, leaveOpen: true))
        {
            using JsonWriter writer = new JsonTextWriter(streamWriter);
            writer.Formatting = _serializer.Formatting;
            _serializer.Serialize(writer, input);
            writer.Flush();
            streamWriter.Flush();
        }

        streamPayload.Position = 0;
        return streamPayload;
    }
}