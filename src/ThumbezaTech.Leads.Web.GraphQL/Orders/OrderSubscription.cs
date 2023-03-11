namespace ThumbezaTech.Leads.Web.GraphQL.Orders;

[ExtendObjectType(OperationTypeNames.Subscription)]
public sealed class OrderSubscription
{
  [Subscribe]
  [Topic("OrderPublishedTopic")]
  [GraphQLName("order_subs")]
  [GraphQLDescription("updated order subscription")]
  public OrderVm OnOrderUpdatedAsync([EventMessage] OrderVm order) => order;
}
