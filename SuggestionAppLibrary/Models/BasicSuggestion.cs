
namespace SuggestionAppLibrary.Models
{
    public class BasicSuggestion
    {
        public string BasicSuggestionId { get; set; }
        public string Suggestion { get; set;}

        public BasicSuggestion()
        {

        }

        public BasicSuggestion(Suggestion suggestion)
        {
            BasicSuggestionId = suggestion.SuggestionId;
            Suggestion = suggestion.SuggestionText;
        }
    }
}
