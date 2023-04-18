using ThumbezaTech.Leads.Domain.OrderAggregate;

namespace ThumbezaTech.Leads.Application.Orders;

public interface IOrderService
{
  ValueTask<Result> CreateOrderAsync(Order order, CancellationToken cancellationToken);
  ValueTask<Result> UpdateOrderAsync(Order order, CancellationToken cancellationToken);
  ValueTask<Result<Order>> GetOrderAsync(string id, CancellationToken cancellationToken);
  ValueTask<Result<IEnumerable<Order>>> GetLeadOrdersAsync(string id, CancellationToken cancellationToken);
  ValueTask<Result<IEnumerable<Order>>> GetOrdersAsync(CancellationToken cancellationToken);
}
