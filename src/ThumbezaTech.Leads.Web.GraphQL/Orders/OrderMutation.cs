using HotChocolate.Subscriptions;

using Mediator;

namespace ThumbezaTech.Leads.Web.GraphQL.Orders;

[ExtendObjectType(OperationTypeNames.Mutation)]
public sealed class OrderMutation
{
  [GraphQLName("add_product")]
  [GraphQLDescription("Add a single product")]
  public async Task<string> AddProduct(
      [Service] ISender Sender,
      [Service] ITopicEventSender topicSender,
      [GraphQLNonNullType] OrderVm order,
      CancellationToken cancellationToken = default)
  {
    var result = await Task.FromResult(Ardalis.Result.Result.Success(Array.Empty<OrderVm>()));
    if (!result.IsSuccess)
    {
      return string.Join("; ", result.Errors);
    }
    await topicSender.SendAsync("OrderPublishedTopic", order, cancellationToken);
    return $"{result.Status}";
  }

  [GraphQLName("update_product")]
  [GraphQLDescription("Update a single product")]
  public async Task<string> UpdateProduct(
      [Service] ISender Sender,
      [Service] ITopicEventSender topicSender,
      [GraphQLNonNullType] OrderVm order,
      CancellationToken cancellationToken = default)
  {
    var result = await Task.FromResult(Ardalis.Result.Result.Success(Array.Empty<OrderVm>()));
    if (!result.IsSuccess)
    {
      return string.Join("; ", result.Errors);
    }

    await topicSender.SendAsync("OrderPublishedTopic", order, cancellationToken);
    return order.Id;
  }
}
