using ThumbezaTech.Leads.ClientLibrary;
using ThumbezaTech.Leads.ClientLibrary.State;
using Microsoft.Extensions.DependencyInjection;

namespace ThumbezaTech.Leads.Web.Server.Features.Products;

public record GetProductQuery(int Page, int Size) : IRequest;

internal sealed class GetProductQueryHandler : IRequestHandler<GetProductQuery>
{
  private readonly ProductState _state;
  private readonly ILogger<GetProductQueryHandler> _logger;
  private readonly IGet_productsQuery _query;

  public GetProductQueryHandler(ProductState state, ILogger<GetProductQueryHandler> logger, IGet_productsQuery query)
  {
    _state = state;
    _logger = logger;
    _query = query;
  }

  public async Task Handle(GetProductQuery request, CancellationToken cancellationToken)
  {
    try
    {
      var content = await _query.ExecuteAsync(cancellationToken);
      if(content.Errors.Any())
      {
        _logger.LogWarning("something didn't go well while fetching products {@Errors}", content.Errors);
      }

      var data = content.Data.List_products.Nodes;
      //await Task.Delay(500, cancellationToken);
      //var content = Enumerable.Range(1, request.Size).Select(i => new ProductVm($"{i}", $"Product {i}", "123456789123", 15.999M));
      //_state.SetStateItems(content.ToArray());
    }
    catch (Exception e)
    {
      _logger.LogError(e, "Something went wrong while fetching products");
    }
  }
}

public record ProductVm(string Id, string Name, string Sku, decimal Price);
