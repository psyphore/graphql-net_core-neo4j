using ThumbezaTech.Leads.Domain.LeadAggregate;

namespace ThumbezaTech.Leads.Application.Leads;
public sealed record CreateLeadCommand(Lead Lead) : ICommand<Result>;

internal sealed class CreateLeadCommandHandler : ICommandHandler<CreateLeadCommand, Result>
{
  private readonly ILeadService _service;

  public CreateLeadCommandHandler(ILeadService service) => _service = service;

  public ValueTask<Result> Handle(CreateLeadCommand command, CancellationToken cancellationToken)
  {
    Guard.Against.Null(command, nameof(command));
    return _service.CreateALeadAsync(Guard.Against.Null(command.Lead, nameof(command.Lead)), cancellationToken);
  }
}
