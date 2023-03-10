namespace ThumbezaTech.Leads.Web.GraphQL.Orders;

[ExtendObjectType(OperationTypeNames.Subscription)]
public sealed class OrderSubscription
{
  [Subscribe]
  [Topic]
  public Task<OrderVm> OnOrderUpdatedAsync(
    [EventMessage] string orderId,
    CancellationToken cancellationToken) => null!;
}
