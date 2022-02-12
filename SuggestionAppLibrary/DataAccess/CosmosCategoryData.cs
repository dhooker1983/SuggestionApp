
namespace SuggestionAppLibrary.DataAccess
{
    public class CosmosCategoryData : ICategoryData
    {
        private readonly ICosmosDbService _cosmosDbService;
        private readonly IMemoryCache _cache;
        private readonly Container _container;
        private const string CacheName = "CategoryData";

        public CosmosCategoryData(ICosmosDbService cosmos, IMemoryCache cache)
        {
            _cache = cache;
            _cosmosDbService = cosmos;
            _container = _cosmosDbService.CategoriesContainer;
        }

        public async Task<List<Category>> GetCategoriesAsync()
        {
            var list = new List<Category>();
            var output = _cache.Get<List<Category>>(CacheName);

            if (output == null)
            {
                var query = new QueryDefinition("SELECT * FROM c");
                var items = _container.GetItemQueryIterator<Category>(query);

                while (items.HasMoreResults)
                {
                    var docs = await items.ReadNextAsync();
                    list.AddRange(docs);
                }

                _cache.Set(CacheName, list, TimeSpan.FromDays(1));
            }

            return list;
        }

        public async Task Create(Category model)
        {
            await _container.CreateItemAsync(model);
        }
    }
}
