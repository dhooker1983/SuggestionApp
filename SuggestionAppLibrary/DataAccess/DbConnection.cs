
using Microsoft.Extensions.Configuration;

namespace SuggestionAppLibrary.DataAccess
{
    public class DbConnection
    {
        private readonly IConfiguration _config;  
        private readonly CosmosClient _client;
        private readonly Container _containerSuggestions;
        private readonly Container _containerStatuses; 
        private readonly string _databaseName;  

        public string CategoryCollectionName { get; private set; } = "categories";
        public string StatusCollectionName { get; private set; } = "statuses";
        public string UserCollectionName { get; private set; } = "users";
        public string SuggestionCollectionName { get; private set; } = "suggestions";

        public DbConnection(IConfiguration config)
        {
            _config = config;
            _client = new CosmosClient(_config.GetConnectionString(""));
            _databaseName = _config["DatabaseName"];
            _containerSuggestions = _client.GetContainer(_databaseName, SuggestionCollectionName);
            _containerStatuses = _client.GetContainer(_databaseName, StatusCollectionName);
        }

    }
}
