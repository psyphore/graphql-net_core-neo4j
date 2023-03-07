using Mediator;

using ThumbezaTech.Leads.Application.Products;

namespace ThumbezaTech.Leads.Web.GraphQL.Products;

/// <summary>
/// Manage Products
/// </summary>
public sealed class ProductsQuery
{
    /// <summary>
    /// Search for products
    /// </summary>
    /// <param name="criteria"></param>
    /// <returns></returns>
    [GraphQLName("products")]
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
