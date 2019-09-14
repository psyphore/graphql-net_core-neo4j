using System.Collections.Generic;

namespace Models.DTOs
{
    public class PersonModel: IDto
    {
        public List<string> Buildings { get; set; }
        public string Firstname { get; set; }
        public string Id { get; set; }
        public string Lastname { get; set; }
        public List<string> Line { get; set; }
        public PersonModel Manager { get; set; }
        public List<string> Products { get; set; }
        public List<string> Team { get; set; }
    }

    public interface IDto
    {

    }
}