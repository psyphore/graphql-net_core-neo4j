using ThumbezaTech.Leads.Application.Orders;
using ThumbezaTech.Leads.Domain.OrderAggregate;

namespace ThumbezaTech.Leads.Infrastructure.Data.Services.Orders;
internal sealed class OrderService : IOrderService
{
  private readonly INeo4jDataAccess _data;
  private const string Label = nameof(Order);

  public OrderService(INeo4jDataAccess data) => _data = data;

  public async ValueTask<Result> CreateOrderAsync(Order order, CancellationToken cancellationToken)
  {
    Dictionary<string, object> input = new()
    {
      { nameof(Order), order.Id},
      { nameof(Order.LineItems), order.LineItems.Select(li => new { li.ProductId, li.Price.Currency, li.Price.Amount }).Serialize() },
      { nameof(Order.Customer), order.Customer?.Serialize() ?? default! },
    };
    var statement = Commands.Options[Commands.SaveOne].Trim();
    var payload = await _data.ExecuteWriteTransactionAsync<string>(statement, input);
    return payload.Any()
        ? Result.SuccessWithMessage(payload)
        : Result.NotFound();
  }

  public async ValueTask<Result<IEnumerable<Order>>> GetLeadOrdersAsync(string id, CancellationToken cancellationToken)
  {
    Dictionary<string, object> Query = new()
    {
      { nameof(id), id },
    };
    var statement = Queries.Options[Queries.LeadOrders].Trim();
    var payload = await _data.ExecuteReadTransactionAsync<Order>(statement, $"{Label}s", Query);
    return payload.Any()
        ? Result.Success(payload.Distinct())
        : Result.NotFound();
  }

  public async ValueTask<Result<Order>> GetOrderAsync(string id, CancellationToken cancellationToken)
  {
    Dictionary<string, object> Query = new()
    {
      { nameof(id), id },
    };
    var statement = Queries.Options[Queries.LeadOrders].Trim();
    var payload = await _data.ExecuteReadTransactionAsync<Order>(statement, $"{Label}s", Query);
    return payload.Any()
        ? Result.Success(payload.First())
        : Result.NotFound();
  }

  public async ValueTask<Result<IEnumerable<Order>>> GetOrdersAsync(CancellationToken cancellationToken)
  {
    var statement = Queries.Options[Queries.LeadOrders].Trim();
    var payload = await _data.ExecuteReadTransactionAsync<Order>(statement, $"{Label}s", null!);
    return payload.Any()
        ? Result.Success(payload.Distinct())
        : Result.NotFound();
  }

  public async ValueTask<Result> UpdateOrderAsync(Order order, CancellationToken cancellationToken)
  {
    Dictionary<string, object> input = new()
    {
      { nameof(Order), order.Id},
      { nameof(Order.LineItems), order.LineItems.Select(li => new { li.ProductId, li.Price.Currency, li.Price.Amount }).Serialize() },
      { nameof(Order.Customer), order.Customer?.Serialize() ?? default! },
    };
    var statement = Commands.Options[Commands.UpdateOne].Trim();
    var payload = await _data.ExecuteWriteTransactionAsync<string>(statement, input);
    return payload is not null
        ? Result.SuccessWithMessage(payload)
        : Result.NotFound();
  }
}
