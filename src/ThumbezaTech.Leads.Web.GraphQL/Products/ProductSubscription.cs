namespace ThumbezaTech.Leads.Web.GraphQL.Products;

[ExtendObjectType(OperationTypeNames.Subscription)]
public sealed class ProductSubscription
{
  [Subscribe]
  [Topic("ProductPublishedTopic")]
  [GraphQLName("product_subs")]
  [GraphQLDescription("published product subscription")]
  public ProductVm ProductPublished([EventMessage] ProductVm product) => product;

}

