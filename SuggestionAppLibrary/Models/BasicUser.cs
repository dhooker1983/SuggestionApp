using Newtonsoft.Json;

namespace SuggestionAppLibrary.Models
{
    public class BasicUser
    {
        [JsonProperty(PropertyName = "basicuserid")]
        public string BasicUserId { get; set; }
        public string DisplayName { get; set; }

        public BasicUser()
        {

        }

        public BasicUser(User user)
        {
            BasicUserId = user.UserId;
            DisplayName = user.DisplayName;
        }
    }
}
