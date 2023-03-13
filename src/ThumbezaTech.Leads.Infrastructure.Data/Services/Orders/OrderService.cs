using ThumbezaTech.Leads.Application.Orders;
using ThumbezaTech.Leads.Domain.OrderAggregate;

namespace ThumbezaTech.Leads.Infrastructure.Data.Services.Orders;
internal sealed class OrderService : IOrderService
{
  private readonly IRepository<Order> _repository;
  private const string Label = nameof(Order);

  public OrderService(IRepository<Order> repository) => _repository = repository;

  public async ValueTask<Result> CreateOrderAsync(Order order, CancellationToken cancellationToken)
  {
    Dictionary<string, object> input = new()
    {
      { nameof(Order), order.Id},
      { nameof(Order.LineItems), order.LineItems.Select(li => new { li.ProductId, li.Price.Currency, li.Price.Amount }).Serialize() },
      { nameof(Order.Customer), order.Customer?.Serialize() ?? default! },
    };

    var statement = Commands.Options[Commands.SaveOne].Trim();
    var records = await _repository.Write(statement, input, cancellationToken);
    var payload = records.Select(record => record.ProcessRecords<string>(Label));

    return payload.Any()
        ? Result.SuccessWithMessage(payload.First())
        : Result.NotFound();
  }

  public async ValueTask<Result<IEnumerable<Order>>> GetLeadOrdersAsync(string id, CancellationToken cancellationToken)
  {
    Dictionary<string, object> Query = new()
    {
      { nameof(id), id },
    };

    var statement = Queries.Options[Queries.LeadOrders].Trim();
    var records = await _repository.Read(statement, Query, cancellationToken);
    var payload = records.Where(record => record is not null).Select(record => record.ProcessRecords<Order>(Label));

    return payload.Any()
        ? Result.Success(payload)
        : Result.NotFound();
  }

  public async ValueTask<Result<Order>> GetOrderAsync(string id, CancellationToken cancellationToken)
  {
    Dictionary<string, object> Query = new()
    {
      { nameof(id), id },
    };

    var statement = Queries.Options[Queries.LeadOrders].Trim();
    var records = await _repository.Read(statement, Query, cancellationToken);
    var payload = records.Where(record => record is not null).Select(record => record.ProcessRecords<Order>($"{Label}s"));

    return payload.Any()
        ? Result.Success(payload.First())
        : Result.NotFound();
  }

  public async ValueTask<Result<IEnumerable<Order>>> GetOrdersAsync(CancellationToken cancellationToken)
  {
    Dictionary<string, object> Query = new();

    var statement = Queries.Options[Queries.LeadOrders].Trim();
    var records = await _repository.Read(statement, Query, cancellationToken);
    var payload = records.Where(record => record is not null).Select(record => record.ProcessRecords<Order>($"{Label}s"));

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
    var records = await _repository.Write(statement, input, cancellationToken);
    var payload = records.Select(record => record.ProcessRecords<string>(Label));

    return payload.Any()
        ? Result.SuccessWithMessage(payload.First())
        : Result.NotFound();
  }
}
