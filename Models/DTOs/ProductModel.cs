using System;
using System.Collections.Generic;

namespace Models.DTOs
{
    public class ProductModel : IDto
    {
        public string Avatar { get; set; }
        public DateTime? Deactivated { get; set; }
        public string Name { get; set; }
        public string Id { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
        public IEnumerable<PersonModel> Champions { get; set; }
        public PersonModel Owner { get; set; }
        public int ChampionCount { get; set; }
    }
}