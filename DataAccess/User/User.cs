using DataAccess.Interfaces;
using Newtonsoft.Json;

namespace DataAccess.User
{
    public class User : IEntity
    {
        [JsonProperty("created")]
        public long Created { get; set; }

        [JsonProperty("deactivated")]
        public long? Deactivated { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("name")]
        public string Firstname { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }
    }
}