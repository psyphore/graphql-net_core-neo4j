using ThumbezaTech.Leads.Domain.ProductAggregate;

namespace ThumbezaTech.Leads.Application.Products;

public sealed record SearchForProductsQuery(string Query, int Page = 1, int Size = 10) : IQuery<Result<IEnumerable<Product>>>;

internal sealed class SearchForProductsQueryHandler : IQueryHandler<SearchForProductsQuery, Result<IEnumerable<Product>>>
{
  private readonly IProductService _service;

  public SearchForProductsQueryHandler(IProductService service) => _service = service;

  public ValueTask<Result<IEnumerable<Product>>> Handle(SearchForProductsQuery query, CancellationToken cancellationToken)
  {
    return _service.QueryProducts(Guard.Against.NullOrEmpty(query.Query), cancellationToken);
  }
}
