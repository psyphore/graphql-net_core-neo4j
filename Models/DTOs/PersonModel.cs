using System;
using System.Collections.Generic;

namespace Models.DTOs
{
    public interface IDto
    {
    }

    public class PersonModel : IDto
    {
        public string Avatar { get; set; }
        public string Bio { get; set; }
        public List<string> Buildings { get; set; }
        public DateTime? Deactivated { get; set; }
        public string Email { get; set; }
        public string Firstname { get; set; }
        public string Id { get; set; }
        public string KnownAs { get; set; }
        public string Lastname { get; set; }
        public IEnumerable<PersonModel> Line { get; set; }
        public PersonModel Manager { get; set; }
        public string Mobile { get; set; }
        public IEnumerable<string> Products { get; set; }
        public IEnumerable<PersonModel> Team { get; set; }
        public string Title { get; set; }
    }
}