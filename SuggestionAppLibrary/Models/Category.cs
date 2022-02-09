using Newtonsoft.Json;

namespace SuggestionAppLibrary.Models
{
    public class Category
    {
        [JsonProperty(PropertyName = "categoryid")]
        public string CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string CategoryDescription { get; set; }
        public string Type { get; set; } = "category";

    }
}
