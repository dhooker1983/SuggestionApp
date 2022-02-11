
namespace SuggestionAppLibrary.DataAccess
{
    public interface IStatusData
    {
        Task CreateStatusAsync(Status model);
        Task<List<Status>> GetStatusesAsync();
    }
}