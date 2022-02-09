
using User = SuggestionAppLibrary.Models.User;

namespace SuggestionAppLibrary.DataAccess
{
    public class CosmosUserData : IUserData
    {
        private readonly ICosmosDbService _cosmosDbService;
        private readonly Container _container;
        private readonly PartitionKey _partitionKey;

        public CosmosUserData(ICosmosDbService cosmos)
        {
            _cosmosDbService = cosmos;
            _container = _cosmosDbService.StatusesContainer;
            _partitionKey = _cosmosDbService.PartitionKey;
        }

        public async Task CreateUserAsync(User model)
        {
             await _container.CreateItemAsync(model);
        }

        public async Task<User> GetUserAsync(string id)
        {
            var user = await _container.ReadItemAsync<User>(id, _partitionKey);

            return user.Resource;
        }

        public async Task<User> GetUserFromAuthentication(string objectId)
        {
            var user = await _container.ReadItemAsync<User>(objectId, _partitionKey);

            return user.Resource;
        }

        public async Task<List<User>> GetUsersAsync()
        {
            var query = new QueryDefinition("SELECT * FROM c");
            var list = _container.GetItemQueryIterator<User>(query);
            var users = new List<User>();

            while (list.HasMoreResults)
            {
                var docs = await list.ReadNextAsync();
                users.AddRange(docs);
            }

            return users;
        }

        public async Task<User> UpdateUserAsync(User model)
        {
            return await _container.UpsertItemAsync(model);
        }


    }
}
