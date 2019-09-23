using GraphQL.Types;
using Models.Types;

namespace GraphQLCore.Unions
{
    public class Queries : UnionGraphType
    {
        public Queries()
        {
            Type<PersonQuery>();
            Type<ProductQuery>();
            Type<BuildingQuery>();
        }
    }
}