using Newtonsoft.Json;

namespace SuggestionAppLibrary.Models
{
    public class User
    {
        [JsonProperty(PropertyName = "userid")]
        public string UserId { get; set; }
        public string ObjectIdentifier { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public string Type { get; set; } = "user";
        public List<BasicSuggestion> AuthoredSuggestions { get; set; } = new();
        public List<BasicSuggestion> VotedOnSuggestions { get; set; } = new();
    }
}
