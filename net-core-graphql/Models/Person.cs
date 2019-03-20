using System;
using System.Collections.Generic;

namespace net_core_graphql.Models
{
    public class Person
    {
        public string Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public DateTime BirthDate { get; set; }
        public string BirthPlace { get; set; }
        public string Height { get; set; }
        public int WeightLbs { get; set; }

        public List<string> Products { get; set; }
        public List<string> Team { get; set; }
        public List<string> Line { get; set; }
        public List<string> Buildings { get; set; }

        public string Manager { get; set; }
    }
}