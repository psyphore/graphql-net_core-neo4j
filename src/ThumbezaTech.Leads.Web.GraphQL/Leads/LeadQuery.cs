using Mediator;

using ThumbezaTech.Leads.Application.Leads;

namespace ThumbezaTech.Leads.Web.GraphQL.Leads;

[ExtendObjectType(OperationTypeNames.Query)]
internal sealed class LeadQuery
{
  [GraphQLName("search_leads")]
  [GraphQLDescription("Search for leads")]
  [UsePaging]
  [UseFiltering]
  public async Task<IQueryable<LeadVm>> SearchLeads(
      [Service] ISender Sender,
      [GraphQLNonNullType] string query,
      CancellationToken cancellationToken = default)
  {
    var content = await Sender.Send(new SearchLeadsQuery(query), cancellationToken);
    return content.IsSuccess
        ? content.Value.Select(item => (LeadVm)item).AsQueryable()
        : Enumerable.Empty<LeadVm>().AsQueryable();
  }

  [GraphQLName("list_leads")]
  [GraphQLDescription("Get all leads")]
  [UsePaging]
  [UseFiltering]
  public async Task<IQueryable<LeadVm>> ListLeads(
      [Service] ISender Sender,
      CancellationToken cancellationToken = default)
  {
    var content = await Sender.Send(new GetLeadsQuery(), cancellationToken);
    return content.IsSuccess
        ? content.Value.Select(item => (LeadVm)item).AsQueryable()
        : Enumerable.Empty<LeadVm>().AsQueryable();
  }

  [GraphQLName("get_lead")]
  [GraphQLDescription("Get lead by Id")]
  public async Task<LeadVm> GetLeadById(
      [Service] ISender Sender,
      [GraphQLNonNullType] string id,
      CancellationToken cancellationToken = default)
  {
    var content = await Sender.Send(new GetLeadByIdQuery(id), cancellationToken);
    return content.IsSuccess
        ? (LeadVm)content.Value
        : default!;
  }
}
