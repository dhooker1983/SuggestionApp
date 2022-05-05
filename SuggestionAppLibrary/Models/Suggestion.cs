using Newtonsoft.Json;

namespace SuggestionAppLibrary.Models
{
    public class Suggestion
    {
        [JsonProperty(PropertyName = "id")]
        public string suggestionid { get; set; }
        public string suggestiontext { get; set; }
        public string description { get; set; }
        public DateTime datecreated { get; set; } = DateTime.UtcNow;
        public Category category { get; set; }
        public  BasicUser author { get; set; }
        public string type { get; set; } = "suggestion";

        [JsonIgnore]
        //HashSet is similar to list but only holds UNIQUE values. 
        public HashSet<string>? uservotes { get; set; }

        public Status suggestionstatus { get; set; }
        public string ownernotes { get; set; }
        public bool approvedforrelease { get; set; } = false;
        public bool archived { get; set; } = false;
        public bool rejected { get; set; } = false;


    }
}
