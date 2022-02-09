
namespace SuggestionAppLibrary.DataAccess
{
    public class CosmosStatusData
    {
        private readonly IMemoryCache _cache;
        private readonly ICosmosDbService _cosmosDbService;
        private readonly Container _container;
        private readonly string CacheName = "StatusData";

        public CosmosStatusData(ICosmosDbService cosmos, IMemoryCache cache)
        {
            _cache = cache;
            _cosmosDbService = cosmos;
            _container = _cosmosDbService.StatusesContainer;
        }

        public async Task<List<Status>> GetStatusesAsync()
        {
            var output = _cache.Get<List<Status>>(CacheName);

            if(output == null)
            {
                var query = new QueryDefinition("SELECT * FROM c");
                var items = _container.GetItemQueryIterator<Status>(query);

                while (items.HasMoreResults)
                {
                    var docs = await items.ReadNextAsync();
                    output.AddRange(docs);
                }

                _cache.Set(CacheName, output, TimeSpan.FromDays(1));
            }

            return output;
        }

        public async Task CreateStatusAsync(Status model)
        {
            await _container.CreateItemAsync(model);
        }
    }
}
