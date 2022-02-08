
namespace SuggestionAppLibrary.DataAccess
{
    public class CosmosCategoryData
    {
        private readonly ICosmosDbService _cosmosDbService;
        private readonly IMemoryCache _cache;
        private readonly Container _container;
        private readonly PartitionKey _partitionKey;
        private const string cacheName = "CategoryData";

        public CosmosCategoryData(ICosmosDbService cosmos, IMemoryCache cache)
        {
            _cache = cache;
            _cosmosDbService = cosmos;
            _container = _cosmosDbService.CategoriesContainer;
            _partitionKey = _cosmosDbService.PartitionKey;
        }

        public async Task<List<Category>> GetCategoriesAsync()
        {
            var output = _cache.Get<List<Category>>(cacheName);
            if (output == null)
            {
                var query = new QueryDefinition("SELECT * FROM c");
                var items = _container.GetItemQueryIterator<Category>(query);

                while (items.HasMoreResults)
                {
                    var docs = await items.ReadNextAsync();
                    output  .AddRange(docs);

                    _cache.Set(cacheName, output, TimeSpan.FromDays(1));
                }
            }

            return output;
        }
    }
}
