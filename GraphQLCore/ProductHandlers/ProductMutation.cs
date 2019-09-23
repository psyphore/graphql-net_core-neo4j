using BusinessServices.Product;
using GraphQL.Types;
using GraphQLCore.Unions;
using Models.GraphQLTypes.Product;

namespace Models.Types
{
    public class ProductMutation : ObjectGraphType, IGraphMutator
    {
        public ProductMutation(IProductService service)
        {
            Name = "ProductMutation";
            Description = "Actions to create, update and delete a Product";

            Field<ProductType>(
                "CreateProduct",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<ProductInputType>> { Name = "Product" }),
                resolve: ctx => {
                    // service.Add(ctx.GetArgument<ProductModel>("Product"))
                    return null;
                },
                description: "Create a new Product"
                );

            Field<ProductType>(
                "UpdateProduct",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<ProductInputType>> { Name = "Product" }),
                resolve: ctx => {
                    // service.Update(ctx.GetArgument<ProductModel>("Product"));
                    return null;
                },
                description: "Update a Product"
                );

            Field<ProductType>(
                "RemoveProduct",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "id" }),
                resolve: ctx => {
                    // service.Delete(ctx.GetArgument<string>("id"));
                    return null;
                },
                description: "Delete a Product"
                );
        }
    }
}