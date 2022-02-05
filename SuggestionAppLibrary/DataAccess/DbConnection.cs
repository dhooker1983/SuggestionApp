
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace SuggestionAppLibrary.DataAccess
{
    public class DbConnection : IDbConnection
    {
        private readonly IConfiguration _config;
        private readonly CosmosDbOptions _cosmosDbOptions;

        public CosmosClient Client { get; private set; }
        public string CategoryCollectionName { get; private set; } = "categories";
        public string StatusCollectionName { get; private set; } = "statuses";
        public string UserCollectionName { get; private set; } = "users";
        public string SuggestionCollectionName { get; private set; } = "suggestions";


        public DbConnection(IConfiguration config, IOptions<CosmosDbOptions> options)
        {
            _config = config;
            _cosmosDbOptions = options.Value;
            Client = new CosmosClient(_config.GetConnectionString(""));

        }

    }
}
