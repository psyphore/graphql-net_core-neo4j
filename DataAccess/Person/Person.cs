using DataAccess.Interfaces;
using System;
using System.Collections.Generic;

namespace DataAccess.Person
{
    public class Person : IEntity
    {
        public DateTime BirthDate { get; set; }
        public string BirthPlace { get; set; }
        public List<string> Buildings { get; set; }
        public string Firstname { get; set; }
        public string Height { get; set; }
        public string Id { get; set; }
        public string Lastname { get; set; }
        public List<string> Line { get; set; }
        public Person Manager { get; set; }
        public List<string> Products { get; set; }
        public List<string> Team { get; set; }
        public int WeightLbs { get; set; }
    }
}