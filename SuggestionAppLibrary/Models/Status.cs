using Newtonsoft.Json;

namespace SuggestionAppLibrary.Models
{
    public class Status
    {
        [JsonProperty(PropertyName = "statusid")]
        public string StatusId { get; set; }
        public string StatusName { get; set; }
        public string StatusDescription { get; set; }
        public string Type { get; set; } = "status";
    }
}
