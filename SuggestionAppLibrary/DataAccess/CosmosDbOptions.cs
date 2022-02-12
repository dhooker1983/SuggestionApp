
namespace SuggestionAppLibrary.DataAccess
{
    public class CosmosDbOptions
    {
        public string CategoriesContainer { get; set; }
        public string Container { get; set;}
        public string CosmosDbConnection { get; set; }
        public string DatabaseName { get; set; }
        public string PartitionKey { get; set; }
        public string StatusesContainer { get; set; }
        public string SuggestionsContainer { get; set; }
        public string UsersContainer { get; set; }

    }
}
