using DataAccess.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace DataAccess.Product
{
    public class Product : IEntity
    {
        [JsonProperty("avatar")]
        public string Avatar { get; set; }

        public DateTime? Deactivated { get; set; }

        [JsonProperty("deactivated")]
        public long? Deactivated2 { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("championCount")]
        public int ChampionCount { get; set; }

        [JsonProperty("champions")]
        public IEnumerable<Person.Person> Champions { get; set; }

        [JsonProperty("owner")]
        public Person.Person Owner { get; set; }

    }
}