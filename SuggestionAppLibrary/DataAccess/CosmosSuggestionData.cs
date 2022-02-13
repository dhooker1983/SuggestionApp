
namespace SuggestionAppLibrary.DataAccess
{
    public class CosmosSuggestionData : ISuggestionData
    {
        private readonly ICosmosDbService _cosmosDbService;
        private readonly Container _container;
        private readonly IMemoryCache _cache;
        private readonly IUserData _userData;
        private readonly PartitionKey _partitionKey;
        private const string CacheName = "SuggestionData";


        public CosmosSuggestionData(ICosmosDbService cosmos, IMemoryCache cache, IUserData userData)
        {
            _cache = cache;
            _cosmosDbService = cosmos;
            _container = _cosmosDbService.SuggestionsContainer;
            _partitionKey = _cosmosDbService.PartitionKey;
            _userData = userData;
        }

        public async Task<List<Suggestion>> GetSuggestionsAsync()
        {
            var list = new List<Suggestion>();
            var output = _cache.Get<List<Suggestion>>(CacheName);

            if (output == null)
            {
                //has tested this query
                var query = new QueryDefinition("SELECT * FROM c WHERE c.archived = 'false'");
                var items = _container.GetItemQueryIterator<Suggestion>(query);

                while (items.HasMoreResults)
                {
                    var docs = await items.ReadNextAsync();
                    list.AddRange(docs);
                }

                _cache.Set(CacheName, list, TimeSpan.FromMinutes(1));
                output = _cache.Get<List<Suggestion>>(CacheName);
            }

            return output;
        }

        public async Task<List<Suggestion>> GetAllApprovedSuggestionsAsync()
        {
            var output = await GetSuggestionsAsync();

            return output.Where(s => s.ApprovedForRelease).ToList();
        }

        public async Task<Suggestion> GetSuggestionAsync(string id)
        {
            return await _container.ReadItemAsync<Suggestion>(id, _partitionKey);
        }

        public async Task<List<Suggestion>> GetAllSuggestionsAwaitingApprovalAsync()
        {
            var output = await GetSuggestionsAsync();

            return output
                    .Where(s => s.ApprovedForRelease == false && s.Rejected == false)
                    .ToList();
        }

        public async Task UpdateSuggestionAsync(Suggestion model)
        {
            await _container.UpsertItemAsync(model);
            _cache.Remove(CacheName);
        }

        //needs testing/researching
        public async Task UpvoteSuggestion(string suggestionid, string user)
        {
            //need to make this a transaction as will be updating two containers simultaneously
            var suggestion = await _container.ReadItemAsync<Suggestion>(suggestionid, _partitionKey);

            bool isUpvote = suggestion.Resource.UserVotes.Add(user);

            //remember hash list only allows unique value pairs
            if (isUpvote == false)
            {
                suggestion.Resource.UserVotes.Remove(user);
            }

            //The below code is how you start a TRANSACTIONAL BATCH OPERTATION IN COSMOS DB
            //_container.CreateTransactionalBatch
        }

        //needs testing/researching
        public async Task CreateSuggestionAsync(Suggestion model)
        {

            var client = _container.CreateTransactionalBatch(_partitionKey);

            TransactionalBatchResponse response = await client.ExecuteAsync();

            using (response)
            {
                if (response.IsSuccessStatusCode)
                {
                    await _container.UpsertItemAsync(model);

                    var user = await _userData.GetUserAsync(model.Author.BasicUserId);
                    user.AuthoredSuggestions.Add(new BasicSuggestion(model));
                    await _userData.UpdateUserAsync(user);
                }
            }

        }

    }
}
