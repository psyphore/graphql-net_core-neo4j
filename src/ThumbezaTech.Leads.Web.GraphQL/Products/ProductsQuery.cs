using Mediator;

using ThumbezaTech.Leads.Application.Products;

namespace ThumbezaTech.Leads.Web.GraphQL.Products;

public sealed class ProductsQuery
{
  [GraphQLName("products")]
  [GraphQLDescription("Search for products")]
  [UsePaging]
  [UseFiltering]
  public async Task<IQueryable<ProductVm>> GetProducts(
      [Service] ISender Sender,
      [GraphQLNonNullType] string query,
      int PageNumber,
      int PageSize,
      CancellationToken cancellationToken = default)
  {
    var content = await Sender.Send(new GetProductsQuery(query, PageNumber, PageSize), cancellationToken);
    return content.IsSuccess
        ? content.Value.Select(item => (ProductVm)item).AsQueryable()
        : Enumerable.Empty<ProductVm>().AsQueryable();
  }
}

