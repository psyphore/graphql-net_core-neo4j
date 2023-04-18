using ThumbezaTech.Leads.Application.Products;
using ThumbezaTech.Leads.Domain.OrderAggregate;

namespace ThumbezaTech.Leads.Application.Orders;

public sealed record UpdateOrderCommand(Order Order) : ICommand<Result>;

internal sealed class UpdateOrderCommandHandler : ICommandHandler<UpdateOrderCommand, Result>
{
  private readonly IOrderService _service;
  private readonly ISender _sender;

  public UpdateOrderCommandHandler(IOrderService service, ISender sender)
  {
    _service = service;
    _sender = sender;
  }

  public async ValueTask<Result> Handle(UpdateOrderCommand query, CancellationToken cancellationToken)
  {
    var matched = await _service.GetOrderAsync(query.Order.Id, cancellationToken);
    if (!matched.IsSuccess)
      return Result.Error(matched.Errors.ToArray());

    foreach (var item in matched.Value.LineItems)
    {
      var product = await _sender.Send(new GetProductByIdQuery(item.ProductId));
      // item.Price;
    }

    return await _service.UpdateOrderAsync(matched.Value, cancellationToken);
  }
}
