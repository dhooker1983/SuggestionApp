using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Options;

namespace SuggestionAppLibrary.DataAccess
{
    public class CosmosDbService : ICosmosDbService
    {
        private readonly CosmosClient _client;
        private readonly CosmosDbOptions _options;

        public CosmosDbService(CosmosClient client, IOptions<CosmosDbOptions> options)
        {
            _client = client;
            _options = options.Value;

            CategoriesContainer = _client.GetContainer(_options.DatabaseName, _options.CategoriesContainer);
            StatusesContainer = _client.GetContainer(_options.DatabaseName, _options.StatusesContainer);
            SuggestionsContainer = _client.GetContainer(_options.DatabaseName, _options.SuggestionsContainer);
            UserContainer = _client.GetContainer(_options.DatabaseName, _options.UsersContainer);

        }

        public Container CategoriesContainer { get; private set; }
        public Container StatusesContainer { get; private set; }
        public Container SuggestionsContainer { get; private set; }
        public Container UserContainer { get; private set; }
        public PartitionKey PartitionKey { get; private set; } = new PartitionKey("");

    }
}
