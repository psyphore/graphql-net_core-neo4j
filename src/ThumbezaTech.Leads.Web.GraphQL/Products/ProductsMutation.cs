using Mediator;

using ThumbezaTech.Leads.Application.Products;
using ThumbezaTech.Leads.Domain.ProductAggregate;

namespace ThumbezaTech.Leads.Web.GraphQL.Products;

[ExtendObjectType(OperationTypeNames.Mutation)]
public sealed class ProductsMutation
{
  [GraphQLName("add_product")]
  [GraphQLDescription("Add a single product")]
  public async Task<string> AddProduct(
      [Service] ISender Sender,
      [GraphQLNonNullType] ProductVm product,
      CancellationToken cancellationToken = default)
  {
    var result = await Sender.Send(new AddProductCommand((Product)product), cancellationToken);
    return result.IsSuccess
      ? $"{result.Status}"
      : string.Join("; ", result.Errors);
  }

  [GraphQLName("update_product")]
  [GraphQLDescription("Update a single product")]
  public async Task<string> UpdateProduct(
      [Service] ISender Sender,
      [GraphQLNonNullType] ProductVm product,
      CancellationToken cancellationToken = default)
  {
    var result = await Sender.Send(new UpdateProductCommand((Product)product), cancellationToken);
    return result.IsSuccess
      ? product.Id
      : string.Join("; ", result.Errors);
  }
}

