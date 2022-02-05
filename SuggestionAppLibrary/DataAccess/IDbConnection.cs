namespace SuggestionAppLibrary.DataAccess
{
    public interface IDbConnection
    {
        CosmosClient Client { get; }
        string CategoryCollectionName { get; }
        string StatusCollectionName { get; }
        string SuggestionCollectionName { get; }
        string UserCollectionName { get; }
    }
}