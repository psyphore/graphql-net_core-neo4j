using ThumbezaTech.Leads.Domain.OrderAggregate;

namespace ThumbezaTech.Leads.Application.Orders;

public sealed record GetOrderByIdQuery(string Id) : IQuery<Result<Order>>;

internal sealed class GetOrderByIdQueryHandler : IQueryHandler<GetOrderByIdQuery, Result<Order>>
{
  private readonly IOrderService _service;
  public GetOrderByIdQueryHandler(IOrderService service) => _service = service;

  public ValueTask<Result<Order>> Handle(GetOrderByIdQuery query, CancellationToken cancellationToken)
  {
    return _service.GetOrderAsync(Guard.Against.NullOrEmpty(query.Id, nameof(query.Id)), cancellationToken);
  }
}
