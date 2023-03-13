using ThumbezaTech.Leads.Domain.LeadAggregate;

namespace ThumbezaTech.Leads.Application.Leads;

public interface ILeadService
{
  ValueTask<Result<IEnumerable<Lead>>> SearchForLeadsAsync(IDictionary<string, object> Query, CancellationToken cancellationToken = default);
  ValueTask<Result> CreateALeadAsync(IDictionary<string, object> Query, CancellationToken cancellationToken = default);
  ValueTask<Result> UpdateLeadAsync(IDictionary<string, object> Query, CancellationToken cancellationToken = default);
  ValueTask<Result<Lead>> GetLeadByIdAsync(IDictionary<string, object> Query, CancellationToken cancellationToken = default);
  ValueTask<Result<IEnumerable<Lead>>> GetLeadsAsync(IDictionary<string, object> Query, CancellationToken cancellationToken = default);
}
