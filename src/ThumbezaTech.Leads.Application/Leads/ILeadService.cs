using ThumbezaTech.Leads.Domain.LeadAggregate;

namespace ThumbezaTech.Leads.Application.Leads;

public interface ILeadService
{
  ValueTask<Result<IEnumerable<Lead>>> SearchForLeadsAsync(string query, CancellationToken cancellationToken = default);
  ValueTask<Result> CreateALeadAsync(Lead lead, CancellationToken cancellationToken = default);
  ValueTask<Result> UpdateLeadAsync(Lead lead, CancellationToken cancellationToken = default);
  ValueTask<Result<Lead>> GetLeadByIdAsync(string id, CancellationToken cancellationToken = default);
  ValueTask<Result<IEnumerable<Lead>>> GetLeadsAsync(CancellationToken cancellationToken = default);
}
