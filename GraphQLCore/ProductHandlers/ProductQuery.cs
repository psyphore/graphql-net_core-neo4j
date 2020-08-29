using BusinessServices.Product;
using GraphQL.Types;
using GraphQLCore.Unions;
using GraphQLCore.GraphQLTypes.Product;

namespace Models.Types
{
    public class ProductQuery : ObjectGraphType, IGraphQueryMarker
    {
        public ProductQuery(IProductService service)
        {
            Name = "ProductnQuery";
            Description = "Actions fetch a Product";

            Field<ProductType>(
                "Product",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "id" }),
                resolve: ctx => service.Get(ctx.GetArgument<string>("id")),
                description: "Fetch a Product by their Identifier"
                );

            Field<ListGraphType<ProductType>>(
                "Products",
                resolve: ctx => service.GetAll(),
                description: "Fetch all Products"
                );
        }
    }
}