using ThumbezaTech.Leads.Domain.LeadAggregate;

namespace ThumbezaTech.Leads.Application.Leads;

public sealed record GetLeadsQuery() : IQuery<Result<IEnumerable<Lead>>>;

internal sealed class GetLeadsQueryHandler : IQueryHandler<GetLeadsQuery, Result<IEnumerable<Lead>>>
{
  private readonly ILeadService _service;
  public GetLeadsQueryHandler(ILeadService service) => _service = service;

  public ValueTask<Result<IEnumerable<Lead>>> Handle(GetLeadsQuery query, CancellationToken cancellationToken)
  {
    return _service.GetLeadsAsync(cancellationToken);
  }
}
