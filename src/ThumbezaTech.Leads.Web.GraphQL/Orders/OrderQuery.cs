using Mediator;

namespace ThumbezaTech.Leads.Web.GraphQL.Orders;

[ExtendObjectType(OperationTypeNames.Query)]
public sealed class OrderQuery
{
  [GraphQLName("list_orders")]
  [GraphQLDescription("Get all orders")]
  [UsePaging]
  [UseFiltering]
  public async Task<IQueryable<OrderVm>> ListOrders(
      [Service] ISender Sender,
      CancellationToken cancellationToken = default)
  {
    var content = await Task.FromResult(Ardalis.Result.Result.Success(Array.Empty<OrderVm>()));
    return content.IsSuccess
      ? content.Value.AsQueryable()
      : Enumerable.Empty<OrderVm>().AsQueryable();
  }

  [GraphQLName("get_order")]
  [GraphQLDescription("Get order by id")]
  public async Task<OrderVm> GetOrderById(
      [Service] ISender Sender,
      [GraphQLNonNullType] string id,
      CancellationToken cancellationToken = default)
  {
    var content = await Task.FromResult(Ardalis.Result.Result.Success(Array.Empty<OrderVm>()));
    return content.IsSuccess
      ? content.Value.FirstOrDefault()
      : default!;
  }
}
