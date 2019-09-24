using System.Collections.Generic;

namespace Models.DTOs
{
    public class SearchCriteriaModel : IDto
    {
        public int First { get; set; } = 9999;
        public int Offset { get; set; } = 0;
        public string Query { get; set; } = "";
    }

    public class SearchModel : IDto
    {
        public int Count { get; set; }
        public string Id { get; set; }
        public IEnumerable<PersonModel> People { get; set; }
    }
}