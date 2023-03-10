using ThumbezaTech.Leads.Domain.ProductAggregate;

namespace ThumbezaTech.Leads.Application.Products;

public sealed record GetProductsQuery(int Page = 1, int Size = 10) : IQuery<Result<IEnumerable<Product>>>;

internal sealed class GetProductQueryHandler : IQueryHandler<GetProductsQuery, Result<IEnumerable<Product>>>
{
  private readonly IProductService _service;

  public GetProductQueryHandler(IProductService service) => _service = service;

  public ValueTask<Result<IEnumerable<Product>>> Handle(GetProductsQuery query, CancellationToken cancellationToken)
  {
    var (Size, Page) = query;
    var parameter = new Dictionary<string, object>
        {
            { "offset", Page },
            { "first", Size }
        };
    return _service.GetProducts(parameter, cancellationToken);
  }
}
