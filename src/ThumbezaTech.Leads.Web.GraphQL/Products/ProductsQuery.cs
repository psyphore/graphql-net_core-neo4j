using Mediator;

using ThumbezaTech.Leads.Application.Products;

namespace ThumbezaTech.Leads.Web.GraphQL.Products;

[ExtendObjectType(OperationTypeNames.Query)]
public sealed class ProductsQuery
{
  [GraphQLName("search_products")]
  [GraphQLDescription("Search for products")]
  [UsePaging]
  [UseFiltering]
  public async Task<IQueryable<ProductVm>> SearchProducts(
      [Service] ISender Sender,
      [GraphQLNonNullType] string query,
      int? PageNumber = 1,
      int? PageSize = 50,
      CancellationToken cancellationToken = default)
  {
    var content = await Sender.Send(new SearchForProductsQuery(query, PageNumber.Value, PageSize.Value), cancellationToken);
    return content.IsSuccess
        ? content.Value.Select(item => (ProductVm)item).AsQueryable()
        : Enumerable.Empty<ProductVm>().AsQueryable();
  }

  [GraphQLName("list_products")]
  [GraphQLDescription("Get all products")]
  [UsePaging]
  [UseFiltering]
  public async Task<IQueryable<ProductVm>> ListProducts(
      [Service] ISender Sender,
      CancellationToken cancellationToken = default)
  {
    var content = await Sender.Send(new GetProductsQuery(), cancellationToken);
    return content.IsSuccess
        ? content.Value.Select(item => (ProductVm)item).AsQueryable()
        : Enumerable.Empty<ProductVm>().AsQueryable();
  }

  [GraphQLName("get_product")]
  [GraphQLDescription("Get product by Id")]
  public async Task<ProductVm> GetProductById(
      [Service] ISender Sender,
      [GraphQLNonNullType] string id,
      CancellationToken cancellationToken = default)
  {
    var content = await Sender.Send(new GetProductByIdQuery(id), cancellationToken);
    return content.IsSuccess
        ? (ProductVm)content.Value
        : default!;
  }
}

