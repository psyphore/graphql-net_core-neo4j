using ThumbezaTech.Leads.Domain.AddressValueObject;
using ThumbezaTech.Leads.Domain.LeadAggregate;

namespace ThumbezaTech.Leads.Application.Leads;
public sealed record CreateLeadCommand(string FirstName, string LastName, DateTimeOffset DateOfBirth, string Email, string MobileNumber, Address Address) : ICommand<Result>;

internal sealed class CreateLeadCommandHandler : ICommandHandler<CreateLeadCommand, Result>
{
  private readonly ILeadService _service;

  public CreateLeadCommandHandler(ILeadService service) => _service = service;

  public ValueTask<Result> Handle(CreateLeadCommand command, CancellationToken cancellationToken)
  {
    var aggregate = new Lead(
      command.FirstName,
      command.LastName,
      command.DateOfBirth,
      command.MobileNumber,
      command.Email,
      command.Address
      );
    return _service.CreateALeadAsync(aggregate, cancellationToken);
  }
}
