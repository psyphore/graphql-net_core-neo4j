﻿using ThumbezaTech.Leads.Domain.LeadAggregate;

namespace ThumbezaTech.Leads.Application.Leads;

public sealed record UpdateLeadCommand(Lead Lead) : ICommand<Result>;

internal sealed class UpdateLeadCommandHandler : ICommandHandler<UpdateLeadCommand, Result>
{
  private readonly ILeadService _service;
  private readonly ISender _sender;

  public UpdateLeadCommandHandler(ILeadService service, ISender sender)
    => (_service, _sender) = (service, sender);

  public async ValueTask<Result> Handle(UpdateLeadCommand command, CancellationToken cancellationToken)
  {
    Guard.Against.Null(command, nameof(command));

    var matched = await _sender.Send(new GetLeadByIdQuery(Guard.Against.Null(command.Lead).Id), cancellationToken);
    if (!matched.IsSuccess)
      return Result.NotFound();

    matched.Value.Update(command.Lead);

    return await _service.UpdateLeadAsync(matched.Value, cancellationToken);
  }
}
