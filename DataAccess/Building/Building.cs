using DataAccess.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace DataAccess.Building
{
    public class Building : IEntity
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("avatar")]
        public string Avatar { get; set; }

        public DateTime? Deactivated { get; set; }

        [JsonProperty("deactivated")]
        public long? Detactivated2 { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("headcount")]
        public int HeadCount { get; set; }

        [JsonProperty("people")]
        public IEnumerable<Person.Person> People { get; set; }
    }
}