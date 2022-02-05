using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Options;

namespace SuggestionAppLibrary.DataAccess
{
    public class CosmosDbService<T> where T : class
    {
        private readonly CosmosClient _client;
        private readonly CosmosDbOptions _options;
        private readonly Container _container;
        private readonly PartitionKey _partitionKey = new PartitionKey("suggestionid");

        public CosmosDbService(CosmosClient client, IOptions<CosmosDbOptions> options)
        {
            _client = client;
            _options = options.Value;
            _container = _client.GetContainer(_options.DatabaseName, _options.SuggestionsContainer);
        }

        public async Task<List<T>> GetAllAsync()
        {
            var query = new QueryDefinition("SELECT * FROM c");
            var items = _container.GetItemQueryIterator<T>(query);
            var results = new List<T>();

            while (items.HasMoreResults)
            {
                var docs = await items.ReadNextAsync();
                results.AddRange(docs);
            }
          
            return results;
        } 

        public async Task<T> GetAsync(string id)
        {
            try
            {
                var item = await _container.ReadItemAsync<T>(id, _partitionKey);

                return item.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        public async Task<T> Create(T model)
        {
            return await _container.CreateItemAsync(model);
        }

        public async Task<T> Update(T model)
        {
            return await _container.UpsertItemAsync(model, _partitionKey);
        }

    }
}
