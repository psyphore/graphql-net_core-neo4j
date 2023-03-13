using ThumbezaTech.Leads.Domain.LeadAggregate;

namespace ThumbezaTech.Leads.Application.Leads;

public sealed record GetLeadByIdQuery(string Id) : IQuery<Result<Lead>>;

internal sealed class GetLeadByIdQueryHandler : IQueryHandler<GetLeadByIdQuery, Result<Lead>>
{
  private readonly ILeadService _service;
  public GetLeadByIdQueryHandler(ILeadService service) => _service = service;

  public ValueTask<Result<Lead>> Handle(GetLeadByIdQuery query, CancellationToken cancellationToken)
  {
    var payload = new Dictionary<string, object>
    {
      { nameof(query.Id), Guard.Against.NullOrEmpty(query.Id, nameof(query.Id)) }
    };
    return _service.GetLeadByIdAsync(payload, cancellationToken);
  }
}
