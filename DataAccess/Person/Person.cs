using DataAccess.Interfaces;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace DataAccess.Person
{
    public class Person : IEntity
    {
        public List<string> Buildings { get; set; }

        [JsonProperty("firstname")]
        public string Firstname { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("lastname")]
        public string Lastname { get; set; }

        public List<string> Line { get; set; }
        public Person Manager { get; set; }
        public List<string> Products { get; set; }
        public List<string> Team { get; set; }
    }
}