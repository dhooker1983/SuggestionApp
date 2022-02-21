
namespace SuggestionAppLibrary.DataAccess
{
    public interface ISuggestionData
    {
        Task CreateSuggestionAsync(Suggestion model);
        Task<List<Suggestion>> GetAllApprovedSuggestionsAsync();
        Task<List<Suggestion>> GetAllSuggestionsAwaitingApprovalAsync();
        Task<Suggestion> GetSuggestionAsync(string id);
        Task<List<Suggestion>> GetSuggestionsAsync();
        Task<List<Suggestion>> GetUsersSuggestions(string userId);
        Task UpdateSuggestionAsync(Suggestion model);
        Task UpvoteSuggestion(string suggestionid, string user);
    }
}