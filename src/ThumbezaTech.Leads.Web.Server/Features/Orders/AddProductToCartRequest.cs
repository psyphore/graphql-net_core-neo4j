namespace ThumbezaTech.Leads.Web.Server.Features.Orders;
public sealed record AddProductToCartRequest(IGet_products_List_products_Nodes Item) : IRequest;

internal sealed class AddProductToCartRequestHandler : IRequestHandler<AddProductToCartRequest>
{
  private readonly OrderState _state;
  private readonly ILogger<AddProductToCartRequestHandler> _logger;

  public AddProductToCartRequestHandler(OrderState state, ILogger<AddProductToCartRequestHandler> logger)
  {
    _state = state;
    _logger = logger;
  }


  public async Task Handle(AddProductToCartRequest request, CancellationToken cancellationToken)
  {
    try
    {
      await Task.Delay(500, cancellationToken);
      _state.SetStateItems(new[] { new OrderVm(Guid.NewGuid().ToString(), request.Item.Id, request.Item.Sku, request.Item.Price.Amount) });
    }
    catch (Exception e)
    {
      _logger.LogError(e, "Something went wrong while adding product to cart");
    }
  }
}
