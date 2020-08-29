using GraphQL.Types;

namespace GraphQLCore.GraphQLTypes.Product
{
    public class ProductInputType : InputObjectGraphType
    {
        public ProductInputType()
        {
            Name = "ProductInput";
            Field<NonNullGraphType<StringGraphType>>("name");
            Field<NonNullGraphType<StringGraphType>>("description");
            Field<StringGraphType>("status");
        }
    }
}