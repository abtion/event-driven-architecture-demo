
using DomainModel.Events;
using DomainModel.Projections;

using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Newtonsoft.Json;

using Storage.CosmosDb;

namespace DomainModel;

public class MusicRequestDomainModelBuilder
{
    private readonly IServiceCollection _services;

    public MusicRequestDomainModelBuilder(IServiceCollection services)
    {
        this._services = services;
    }

    public MusicRequestDomainModelBuilder UseCosmosDb(IConfigurationSection configurationSection)
    {
        _services.AddSingleton(InitializeCosmosClientInstanceAsync(configurationSection).GetAwaiter().GetResult());

        return this;
    }

    private static async Task<ICosmosDbService> InitializeCosmosClientInstanceAsync(IConfigurationSection configurationSection)
    {
        string databaseName = configurationSection.GetSection("DatabaseName").Value;
        string eventsContainerName = configurationSection.GetSection("EventsContainerName").Value;
        string projectionsContainerName = configurationSection.GetSection("ProjectionsContainerName").Value;
        string account = configurationSection.GetSection("Account").Value;
        string key = configurationSection.GetSection("Key").Value;

        var serializerSettings = new JsonSerializerSettings
        {
            Converters =
            {
                new EventJsonConverter()
            }
        };
        var clientOptions = new CosmosClientOptions
        {
            Serializer = new NewtonsoftJsonCosmosSerializer(serializerSettings)
        };

        var client = new CosmosClient(account, key, clientOptions);

        DatabaseResponse database = await client.CreateDatabaseIfNotExistsAsync(databaseName);
        await database.Database.DeleteAsync();
        database = await client.CreateDatabaseIfNotExistsAsync(databaseName);

        await database.Database.CreateContainerIfNotExistsAsync(eventsContainerName, "/partitionKey");
        await database.Database.CreateContainerIfNotExistsAsync(projectionsContainerName, "/partitionKey");

        var eventContainer = new CosmosDbContainerService<EventBase>(client, databaseName, eventsContainerName);
        var projectionsContainer = new CosmosDbContainerService<ProjectionBase>(client, databaseName, projectionsContainerName);

        return new CosmosDbService(eventContainer, projectionsContainer);
    }
}