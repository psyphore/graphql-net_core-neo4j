using HotChocolate.Subscriptions;

using Mediator;

using ThumbezaTech.Leads.Application.Products;
using ThumbezaTech.Leads.Domain.ProductAggregate;

namespace ThumbezaTech.Leads.Web.GraphQL.Products;

[ExtendObjectType(OperationTypeNames.Mutation)]
public sealed class ProductsMutation
{
  [GraphQLName("add_product")]
  [GraphQLDescription("Add a single product")]
  public async Task<string> AddProduct(
      [Service] ISender Sender,
      [Service] ITopicEventSender topicSender,
      [GraphQLNonNullType] ProductVm product,
      CancellationToken cancellationToken = default)
  {
    var result = await Sender.Send(new AddProductCommand((Product)product), cancellationToken);
    if (!result.IsSuccess)
    {
      return string.Join("; ", result.Errors);
    }
    await topicSender.SendAsync("ProuctPublishedTopic", product, cancellationToken);
    return $"{result.Status}";
  }

  [GraphQLName("update_product")]
  [GraphQLDescription("Update a single product")]
  public async Task<string> UpdateProduct(
      [Service] ISender Sender,
      [Service] ITopicEventSender topicSender,
      [GraphQLNonNullType] ProductVm product,
      CancellationToken cancellationToken = default)
  {
    var result = await Sender.Send(new UpdateProductCommand((Product)product), cancellationToken);
    if (!result.IsSuccess)
    {
      return string.Join("; ", result.Errors);
    }

    await topicSender.SendAsync("ProuctPublishedTopic", product, cancellationToken);
    return product.Id;
  }
}

