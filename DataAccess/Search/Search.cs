using DataAccess.Interfaces;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace DataAccess.Search
{
    public class Search : IEntity
    {
        public string Id { get; set; } = "";

        [JsonProperty("person")]
        public IEnumerable<Person.Person> People { get; set; }
    }
}