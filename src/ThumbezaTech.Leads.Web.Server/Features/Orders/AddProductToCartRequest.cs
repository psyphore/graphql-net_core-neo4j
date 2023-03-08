using ThumbezaTech.Leads.Web.Server.Features.Products;

namespace ThumbezaTech.Leads.Web.Server.Features.Orders;
public sealed record AddProductToCartRequest(ProductVm Item) : IRequest;

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
      _state.SetStateItems(new[] { new OrderVm(Guid.NewGuid().ToString(), request.Item.Id, request.Item.Sku, request.Item.Price) });
    }
    catch (Exception e)
    {
      _logger.LogError(e, "Something went wrong while adding product to cart");
    }
  }
}
