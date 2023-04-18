using ThumbezaTech.Leads.Domain.OrderAggregate;

namespace ThumbezaTech.Leads.Application.Orders;
public sealed record CreateOrderCommand(Order Order) : ICommand<Result>;

internal sealed class CreateOrderCommandHandler : ICommandHandler<CreateOrderCommand, Result>
{
  private readonly IOrderService _service;
  public CreateOrderCommandHandler(IOrderService service) => _service = service;

  public ValueTask<Result> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
  {
    return _service.CreateOrderAsync(Guard.Against.Null(command.Order, nameof(command.Order)), cancellationToken);
  }
}
