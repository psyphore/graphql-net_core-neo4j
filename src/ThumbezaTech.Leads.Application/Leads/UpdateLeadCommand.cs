using Newtonsoft.Json;

using ThumbezaTech.Leads.Domain.LeadAggregate;

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
    var matched = await _sender.Send(new GetLeadByIdQuery(command.Lead.Id), cancellationToken);
    if (!matched.IsSuccess)
      return Result.NotFound();

    matched.Value.Update(command.Lead);

    Dictionary<string, object> payload = new()
    {
      { nameof(Lead), JsonConvert.SerializeObject(matched.Value) }
    };
    return await _service.UpdateLeadAsync(payload, cancellationToken);
  }
}
