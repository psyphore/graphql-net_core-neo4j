using ThumbezaTech.Leads.Domain.ProductAggregate;

namespace ThumbezaTech.Leads.Application.Products;

public sealed record SearchForProductsQuery(string Query, int Page = 1, int Size = 10) : IQuery<Result<IEnumerable<Product>>>;

internal sealed class SearchForProductsQueryHandler : IQueryHandler<SearchForProductsQuery, Result<IEnumerable<Product>>>
{
  private readonly IProductService _service;

  public SearchForProductsQueryHandler(IProductService service) => _service = service;

  public ValueTask<Result<IEnumerable<Product>>> Handle(SearchForProductsQuery query, CancellationToken cancellationToken)
  {
    var (Size, Page, Query) = query;
    var parameter = new Dictionary<string, object>
        {
            { "offset", Page },
            { "first", Size },
            { "query", Query }
        };
    return _service.QueryProducts(parameter, cancellationToken);
  }
}
