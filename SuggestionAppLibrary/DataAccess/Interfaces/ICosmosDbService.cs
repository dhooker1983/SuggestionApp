
namespace SuggestionAppLibrary.DataAccess
{
    public interface ICosmosDbService
    {
        Container CategoriesContainer { get; }
        PartitionKey PartitionKey { get; }
        Container StatusesContainer { get; }
        Container SuggestionsContainer { get; }
        Container UserContainer { get; }
    }
}