using GraphQL.Types;
using System.Collections.Generic;

namespace GraphQLCore.Unions
{
    public class CompositeQueries: ObjectGraphType
    {
        public CompositeQueries(IEnumerable<IGraphQueryMarker> queries)
        {
            Name = "Queries";
            Description = "Read only fetch actions";

            foreach (var query in queries)
            {
                var q = query as ObjectGraphType;
                foreach(var field in q.Fields)
                    AddField(field);
            }
        }
    }
}