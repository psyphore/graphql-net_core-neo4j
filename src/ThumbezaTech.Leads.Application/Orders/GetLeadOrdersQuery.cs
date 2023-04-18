using ThumbezaTech.Leads.Domain.OrderAggregate;

namespace ThumbezaTech.Leads.Application.Orders;

public sealed record GetLeadOrdersQuery(string Id) : IQuery<Result<IEnumerable<Order>>>;

internal sealed class GetLeadOrdersQueryHandler : IQueryHandler<GetLeadOrdersQuery, Result<IEnumerable<Order>>>
{
  private readonly IOrderService _service;
  public GetLeadOrdersQueryHandler(IOrderService service) => _service = service;

  public ValueTask<Result<IEnumerable<Order>>> Handle(GetLeadOrdersQuery query, CancellationToken cancellationToken)
  {
    return _service.GetLeadOrdersAsync(Guard.Against.NullOrEmpty(query.Id, nameof(query.Id)), cancellationToken);
  }
}
