using GraphQL.Types;

namespace Models.GraphQLTypes.Product
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