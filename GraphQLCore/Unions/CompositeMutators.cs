using GraphQL.Types;
using System.Collections.Generic;

namespace GraphQLCore.Unions
{
    public class CompositeMutators : ObjectGraphType
    {
        public CompositeMutators(IEnumerable<IGraphMutator> mutators)
        {
            Name = "Mutators";
            Description = "Actions with side-effects";

            foreach (var query in mutators)
            {
                var q = query as ObjectGraphType;
                foreach (var field in q.Fields)
                    AddField(field);
            }
        }
    }
}