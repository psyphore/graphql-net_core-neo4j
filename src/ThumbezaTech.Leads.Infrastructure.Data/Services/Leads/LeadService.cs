﻿using Mediator;

using ThumbezaTech.Leads.Application.Leads;
using ThumbezaTech.Leads.Domain.LeadAggregate;

namespace ThumbezaTech.Leads.Infrastructure.Data.Services.Leads;

internal sealed class LeadService : ILeadService
{
  private readonly INeo4jDataAccess _data;
  private readonly IPublisher _publisher;
  private const string Label = nameof(Lead);

  public LeadService(INeo4jDataAccess data, IPublisher publisher)
    => (_data, _publisher) = (data, publisher);

  public async ValueTask<Result<Lead>> GetLeadByIdAsync(string id, CancellationToken cancellationToken = default)
  {
    Dictionary<string, object> Query = new()
    {
      { nameof(id), id },
    };
    var statement = Queries.Options[Queries.GetOne].Trim();
    var payload = await _data.ExecuteReadTransactionAsync<Lead>(statement, Label, Query);
    return payload.Any()
        ? Result.Success(payload.First())
        : Result.NotFound();
  }

  public async ValueTask<Result<IEnumerable<Lead>>> GetLeadsAsync(CancellationToken cancellationToken = default)
  {
    var statement = Queries.Options[Queries.GetAll].Trim();
    var payload = await _data.ExecuteReadTransactionAsync<Lead>(statement, $"{Label}s", null!);
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
    var payload = await _data.ExecuteReadTransactionAsync<Lead>(statement, $"{Label}s", Query);
    return payload.Any(p => p is not null && !string.IsNullOrEmpty(p.Id))
        ? Result.Success(payload.Distinct())
        : Result.NotFound();
  }

  public async ValueTask<Result> CreateALeadAsync(Lead lead, CancellationToken cancellationToken = default)
  {
    var events = lead.GetEvents();
    lead.ClearEvents();

    Dictionary<string, object> input = new()
    {
      { nameof(Lead), lead.Serialize() },
    };
    var statement = Commands.Options[Commands.SaveOne].Trim();
    var payload = await _data.ExecuteWriteTransactionAsync<string>(statement, input);
    foreach (var item in events)
    {
      await _publisher.Publish(item, cancellationToken);
    }
    return payload.Any()
        ? Result.SuccessWithMessage(payload)
        : Result.NotFound();
  }

  public async ValueTask<Result> UpdateLeadAsync(Lead lead, CancellationToken cancellationToken = default)
  {
    var events = lead.GetEvents();
    lead.ClearEvents();

    Dictionary<string, object> input = new()
    {
      { nameof(Lead), lead.Serialize() },
    };
    var statement = Commands.Options[Commands.UpdateOne].Trim();
    var payload = await _data.ExecuteWriteTransactionAsync<string>(statement, input);
    foreach (var item in events)
    {
      await _publisher.Publish(item, cancellationToken);
    }
    return payload.Any()
        ? Result.SuccessWithMessage(payload)
        : Result.NotFound();
  }

}
