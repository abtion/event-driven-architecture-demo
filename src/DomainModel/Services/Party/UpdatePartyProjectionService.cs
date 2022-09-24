using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DomainModel.Documents.Party;
using DomainModel.Projections;

using Storage.CosmosDb;

namespace DomainModel.Services.Party
{
    public class UpdatePartyProjectionService
    {
        private readonly ICosmosDbService _cosmosDbService;

        public UpdatePartyProjectionService(ICosmosDbService cosmosDbService)
        {
            _cosmosDbService = cosmosDbService;
        }

        public async Task AddSongRequestToProjection(SongRequestedDocument songRequested)
        {
            var projection = await LoadProjection(songRequested.PartitionKey);

            if (projection is not null)
            {
                projection.RequestedSongs.Add(new SongRequestProjection(songRequested.SongTitle, songRequested.Artist, songRequested.Created));

                await _cosmosDbService.ProjectionContainerService.UpdateItemAsync(projection.Id, projection);
            }
        }

        public async Task AddSongPlayedToProjection(SongPlayedDocument songRequestMet)
        {
            var projection = await LoadProjection(songRequestMet.PartitionKey);

            if (projection is not null)
            {
                projection.RequestedSongs.RemoveAll(p => p.SongTitle == songRequestMet.SongTitle && p.ArtistName == songRequestMet.Artist);
                projection.PlayedSongs.Add(new PlayedSongProjection(songRequestMet.SongTitle, songRequestMet.Artist, songRequestMet.Created));

                await _cosmosDbService.ProjectionContainerService.UpdateItemAsync(projection.Id, projection);
            }
        }

        public async Task AddSongDeniedToProjection(SongDeniedDocument songRequestDenied)
        {
            var projection = await LoadProjection(songRequestDenied.PartitionKey);

            if (projection is not null)
            {
                projection.RequestedSongs.RemoveAll(p => p.SongTitle == songRequestDenied.SongTitle && p.ArtistName == songRequestDenied.Artist);
                projection.DeniedSongs.Add(new DeniedSongsProjection(songRequestDenied.SongTitle, songRequestDenied.Artist, songRequestDenied.ReasonForNotPlayingSong));

                await _cosmosDbService.ProjectionContainerService.UpdateItemAsync(projection.Id, projection);
            }
        }

        public async Task UpdatePartyOnProjection(PartyUpdatedDocument partyUpdated)
        {
            var projection = await LoadProjection(partyUpdated.PartitionKey);

            if (projection is not null)
            {
                projection.Name = partyUpdated.Name;

                await _cosmosDbService.ProjectionContainerService.UpdateItemAsync(projection.Id, projection);
            }
        }

        public async Task CreatePartyProjection(PartyCreatedDocument partyCreated)
        {
            var projection = new PartyProjection(partyCreated.Id, partyCreated.Name, new(), new(), new());

            await _cosmosDbService.ProjectionContainerService.AddItemAsync(projection);
        }

        private async Task<PartyProjection?> LoadProjection(string partyId)
        {
            var item = await _cosmosDbService.ProjectionContainerService.GetItemAsync(partyId);

            if (item is PartyProjection)
                return (PartyProjection)item;

            return null;
        }
    }
}