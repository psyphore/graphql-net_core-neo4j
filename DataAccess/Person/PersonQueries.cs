using System.Collections.Generic;

namespace DataAccess.Person
{
    public class PersonQueries
    {
        public IDictionary<string, string> Mutations
        {
            get
            {
                return new Dictionary<string, string>
                {
                    { "MERGE_PERSON", @"CREATE (p:Person) SET p = $p RETURN p"},
                    { "DELETE_PERSON", @""},
                    { "DEACTIVATE_PERSON", @""}
                };
            }
        }

        public IDictionary<string, string> Queries
        {
            get
            {
                return new Dictionary<string, string>
                {
                    { "GET_PERSON", @"MATCH (p:Person {id = $id}) RETURN p"},
                    { "GET_PEOPLE", @"MATCH (p:Person) RETURN p" }
                };
            }
        }
    }
}