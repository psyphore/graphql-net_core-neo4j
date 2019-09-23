using System;
using System.Collections.Generic;

namespace Models.DTOs
{

    public class BuildingModel : IDto
    {
        public string Avatar { get; set; }
        public DateTime? Deactivated { get; set; }
        public string Address { get; set; }
        public string Name { get; set; }
        public string Id { get; set; }
        public int HeadCount { get; set; }
        public IEnumerable<PersonModel> People { get; set; }
    }
}