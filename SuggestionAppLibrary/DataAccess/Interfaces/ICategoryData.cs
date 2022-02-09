
namespace SuggestionAppLibrary.DataAccess
{
    public interface ICategoryData
    {
        Task Create(Category model);
        Task<List<Category>> GetCategoriesAsync();
    }
}