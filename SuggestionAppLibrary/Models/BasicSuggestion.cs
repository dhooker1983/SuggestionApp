using Newtonsoft.Json;

namespace SuggestionAppLibrary.Models
{
    public class BasicSuggestion
    {
        [JsonProperty(PropertyName = "id")]
        public string BasicSuggestionId { get; set; }
        public string Suggestion { get; set;}

        public BasicSuggestion()
        {

        }

        //this constructor allows you to create a basic/lighter suggestion from a full Suggestion Object.
        public BasicSuggestion(Suggestion suggestion)
        {
            BasicSuggestionId = suggestion.SuggestionId;
            Suggestion = suggestion.SuggestionText;
        }
    }
}
