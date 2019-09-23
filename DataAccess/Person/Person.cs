using DataAccess.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace DataAccess.Person
{
    public class Person : IEntity
    {
        [JsonProperty("avatar")]
        public string Avatar { get; set; }

        [JsonProperty("bio")]
        public string Bio { get; set; }

        [JsonProperty("building")]
        public IEnumerable<Building.Building> Buildings { get; set; }

        public DateTime? Deactivated { get; set; }

        [JsonProperty("deactivated")]
        public long? Deactivated2 { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("firstname")]
        public string Firstname { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("knownAs")]
        public string KnownAs { get; set; }

        [JsonProperty("lastname")]
        public string Lastname { get; set; }

        [JsonIgnore]
        [JsonProperty("line")]
        public IEnumerable<Person> Line { get; set; }

        [JsonIgnore]
        [JsonProperty("manager")]
        public Person Manager { get; set; }

        [JsonProperty("mobile")]
        public string Mobile { get; set; }

        [JsonIgnore]
        [JsonProperty("products")]
        public IEnumerable<Product.Product> Products { get; set; }

        [JsonIgnore]
        [JsonProperty("team")]
        public IEnumerable<Person> Team { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }
    }
}