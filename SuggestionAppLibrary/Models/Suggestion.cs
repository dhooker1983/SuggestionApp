using Newtonsoft.Json;

namespace SuggestionAppLibrary.Models
{
    public class Suggestion
    {
        [JsonProperty(PropertyName = "suggestionid")]
        public string SuggestionId { get; set; }
        public string SuggestionText { get; set; }
        public string Description { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
        public Category Category { get; set; }
        public string Author { get; set; }
        
        //HashSet is similar to list but only holds UNIQUE values. 
        public HashSet<string> UserVotes { get; set; }

        public Status SuggestionStatus { get; set; }
        public string OwnerNotes { get; set; }
        public bool ApprovedForRelease { get; set; } = false;
        public bool Archived { get; set; } = false;
        public bool Rejected { get; set; } = false;


    }
}
