using ThumbezaTech.Leads.Domain.LeadAggregate;

namespace ThumbezaTech.Leads.Application.Leads;

public sealed record GetLeadByIdQuery(string Id) : IQuery<Result<Lead>>;

internal sealed class GetLeadByIdQueryHandler : IQueryHandler<GetLeadByIdQuery, Result<Lead>>
{
  private readonly ILeadService _service;
  public GetLeadByIdQueryHandler(ILeadService service) => _service = service;

  public ValueTask<Result<Lead>> Handle(GetLeadByIdQuery query, CancellationToken cancellationToken)
  {
    return _service.GetLeadByIdAsync(Guard.Against.NullOrEmpty(query.Id, nameof(query.Id)), cancellationToken);
  }
}
