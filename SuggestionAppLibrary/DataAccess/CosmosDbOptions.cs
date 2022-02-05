
namespace SuggestionAppLibrary.DataAccess
{
    public class CosmosDbOptions
    {
        public string DatabaseName { get; set; }
        public string CosmosDbConnection { get; set; }
        public string CategoriesContainer { get; set; }
        public string StatusesContainer { get; set; }
        public string SuggestionsContainer { get; set; }
        public string UsersContainer { get; set; }

    }
}
