using Mediator;

using ThumbezaTech.Leads.Application.Products;
using ThumbezaTech.Leads.Domain.ProductAggregate;

namespace ThumbezaTech.Leads.Web.GraphQL.Products;

public sealed class ProductsMutation
{
  [GraphQLName("update_product")]
  [GraphQLDescription("Update a single product")]
  public async Task UpdateProduct(
      [Service] ISender Sender,
      [GraphQLNonNullType] ProductVm product,
      CancellationToken cancellationToken = default)
  {
    await Sender.Send(new UpdateProductCommand((Product)product), cancellationToken);
  }

  [GraphQLName("add_product")]
  [GraphQLDescription("Add a single product")]
  public async Task AddProduct(
      [Service] ISender Sender,
      [GraphQLNonNullType] ProductVm product,
      CancellationToken cancellationToken = default)
  {
    await Sender.Send(new AddProductCommand((Product)product), cancellationToken);
  }
}

