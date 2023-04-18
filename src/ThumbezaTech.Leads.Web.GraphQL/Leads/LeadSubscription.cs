namespace ThumbezaTech.Leads.Web.GraphQL.Leads;

[ExtendObjectType(OperationTypeNames.Subscription)]
internal sealed class LeadSubscription
{
  [Subscribe]
  [Topic("OnLeadPublishedTopic")]
  [GraphQLName("lead_subs")]
  [GraphQLDescription("published lead subscription")]
  public LeadVm OnLeadPublished([EventMessage] LeadVm lead) => lead;
}
