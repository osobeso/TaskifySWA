namespace Taskify.AzureTables.Repositories
{
    using AutoMapper;
    using Azure.Data.Tables;
    using System;
    using System.Threading.Tasks;
    using Taskify.AzureTables.Entities;
    using Taskify.Data.Models;
    using Taskify.Data.Repositories;
    internal class UserProfileStorageRepository : IUserProfileRepository
    {
        private readonly TableClient Table;
        private readonly IMapper Mapper;
        private const string UserProfile = nameof(UserProfile);
        public UserProfileStorageRepository(IAzureTableStorageService storageService, IMapper mapper)
        {
            Table = storageService.GetClient().GetTableClient(UserProfile);
            Mapper = mapper;
        }

        public static void CreateTableIfNotExists(IAzureTableStorageService service)
        {
            var client = service.GetClient();
            var table = client.GetTableClient(UserProfile);
            table.CreateIfNotExists();
        }

        public async Task<UserProfileModel> UpsertAsync(UserProfileModel userProfile)
        {
            var entity = Mapper.Map<UserProfileEntity>(userProfile);
            var response = await Table.UpsertEntityAsync(entity, TableUpdateMode.Replace);
            if (response.IsError)
            {
                throw new Exception($"Error ocured while upserting element: {response.Content}");
            }
            return userProfile;
        }

        public Task<UserProfileModel?> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
