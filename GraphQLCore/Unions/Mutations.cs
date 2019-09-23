using GraphQL.Types;
using Models.Types;

namespace GraphQLCore.Unions
{
    public class Mutations: UnionGraphType
    {
        public Mutations()
        {
            Type<PersonMutation>();
            Type<ProductMutation>();
            Type<BuildingMutation>();
        }
    }
}