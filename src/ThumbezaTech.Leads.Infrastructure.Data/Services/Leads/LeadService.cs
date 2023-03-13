using ThumbezaTech.Leads.Application.Leads;
using ThumbezaTech.Leads.Domain.AddressValueObject;
using ThumbezaTech.Leads.Domain.LeadAggregate;

namespace ThumbezaTech.Leads.Infrastructure.Data.Services.Leads;
internal sealed class LeadService : ILeadService
{
  private readonly IRepository<Lead> _repository;
  private const string Label = nameof(Lead);

  public LeadService(IRepository<Lead> repository) => _repository = repository;

  public async ValueTask<Result<Lead>> GetLeadByIdAsync(string id, CancellationToken cancellationToken = default)
  {
    Dictionary<string, object> Query = new()
    {
      { nameof(id), id },
    };
    var statement = Queries.Options[Queries.GetOne].Trim();
    var records = await _repository.Read(statement, Query, cancellationToken);
    var payload = records.Where(record => record is not null).Select(record => record.ProcessRecords<Lead>(Label));

    return payload is not null
        ? Result.Success(payload.First())
        : Result.NotFound();
  }

  public async ValueTask<Result<IEnumerable<Lead>>> GetLeadsAsync(CancellationToken cancellationToken = default)
  {
    Dictionary<string, object> Query = new Dictionary<string, object>();

    var statement = Queries.Options[Queries.GetAll].Trim();
    var records = await _repository.Read(statement, Query, cancellationToken);
    var payload = records.Where(record => record is not null).Select(record => record.ProcessRecords<Lead>($"{Label}s"));

    return payload.Any()
        ? Result.Success(payload.Distinct())
        : Result.NotFound();
  }

  public async ValueTask<Result<IEnumerable<Lead>>> SearchForLeadsAsync(string query, CancellationToken cancellationToken = default)
  {
    Dictionary<string, object> Query = new()
    {
      { nameof(query), query },
    };

    var statement = Queries.Options[Queries.Search].Trim();
    var records = await _repository.Read(statement, Query, cancellationToken);
    var payload = records.Where(record => record is not null).Select(record => record.ProcessRecords<Lead>($"{Label}s"));

    return payload.Any()
        ? Result.Success(payload.Distinct())
        : Result.NotFound();
  }
  
  public async ValueTask<Result> CreateALeadAsync(Lead lead, CancellationToken cancellationToken = default)
  {
    Dictionary<string, object> input = new()
    {
      { nameof(Lead), lead.Serialize() },
    };
    var statement = Commands.Options[Commands.SaveOne].Trim();
    var records = await _repository.Write(statement, input, cancellationToken);
    var payload = records.Select(record => record.ProcessRecords<Lead>(Label));

    return payload.Any()
        ? Result.SuccessWithMessage(payload.First().Id)
        : Result.NotFound();
  }

  public async ValueTask<Result> UpdateLeadAsync(Lead lead, CancellationToken cancellationToken = default)
  {
    Dictionary<string, object> input = new()
    {
      { nameof(Lead), lead.Serialize() },
    };
    var statement = Commands.Options[Commands.UpdateOne].Trim();
    var records = await _repository.Write(statement, input, cancellationToken);
    var payload = records.Select(record => record.ProcessRecords<Lead>(Label));

    return payload.Any()
        ? Result.SuccessWithMessage(payload.First().Id)
        : Result.NotFound();
  }
}
