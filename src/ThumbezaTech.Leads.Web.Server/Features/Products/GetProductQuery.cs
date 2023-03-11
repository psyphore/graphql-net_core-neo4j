namespace ThumbezaTech.Leads.Web.Server.Features.Products;

public record GetProductQuery(int Page, int Size) : IRequest;

internal sealed class GetProductQueryHandler : IRequestHandler<GetProductQuery>
{
  private readonly ProductState _state;
  private readonly ILogger<GetProductQueryHandler> _logger;
  private readonly ILeadsClient _client;

  public GetProductQueryHandler(ProductState state, ILogger<GetProductQueryHandler> logger, ILeadsClient client)
  {
    _state = state;
    _logger = logger;
    _client = client;
  }

  public async Task Handle(GetProductQuery request, CancellationToken cancellationToken)
  {
    try
    {
      var content = await _client.Get_products.ExecuteAsync(cancellationToken);
      if (content.Errors.Any())
      {
        _logger.LogWarning("something didn't go well while fetching products {@Errors}", content.Errors);
        return;
      }

      var data = content.Data.List_products.Nodes;
      _state.SetStateItems(data.ToArray());
    }
    catch (Exception e)
    {
      _logger.LogError(e, "Something went wrong while fetching products");
    }
  }
}
