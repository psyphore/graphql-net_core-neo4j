using ThumbezaTech.Leads.Domain.ProductAggregate;

namespace ThumbezaTech.Leads.Application.Products;

public sealed record GetProductByIdQuery(string Id) : IQuery<Result<Product>>;

internal sealed class GetProductByIdQueryHandler : IQueryHandler<GetProductByIdQuery, Result<Product>>
{
  private readonly IProductService _service;

  public GetProductByIdQueryHandler(IProductService service) => _service = service;

  public ValueTask<Result<Product>> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
  {
    var parameter = new Dictionary<string, object>
        {
            { "id", query.Id }
        };
    return _service.GetProductById(parameter, cancellationToken);
  }
}
