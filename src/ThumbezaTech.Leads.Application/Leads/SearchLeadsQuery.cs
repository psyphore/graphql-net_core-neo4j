using ThumbezaTech.Leads.Domain.LeadAggregate;

namespace ThumbezaTech.Leads.Application.Leads;

public sealed record SearchLeadsQuery(string Query) : IQuery<Result<IEnumerable<Lead>>>;

internal sealed class SearchLeadsQueryHandler : IQueryHandler<SearchLeadsQuery, Result<IEnumerable<Lead>>>
{
  private readonly ILeadService _service;
  public SearchLeadsQueryHandler(ILeadService service) => _service = service;

  public ValueTask<Result<IEnumerable<Lead>>> Handle(SearchLeadsQuery query, CancellationToken cancellationToken)
  {
    return _service.SearchForLeadsAsync(new Dictionary<string, object>
    {
      { nameof(query), query.Query }
    }, cancellationToken);
  }
}
