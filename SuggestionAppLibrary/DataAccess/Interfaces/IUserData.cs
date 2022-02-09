
using User = SuggestionAppLibrary.Models.User;

namespace SuggestionAppLibrary.DataAccess
{
    public interface IUserData
    {
        Task CreateUserAsync(User model);
        Task<User> GetUserAsync(string id);
        Task<User> GetUserFromAuthentication(string objectId);
        Task<List<User>> GetUsersAsync();
        Task<User> UpdateUserAsync(User model);
    }
}