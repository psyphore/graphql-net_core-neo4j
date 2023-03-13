using Newtonsoft.Json;

using ThumbezaTech.Leads.Domain.LeadAggregate;

namespace ThumbezaTech.Leads.Application.Leads;
public sealed record CreateLeadCommand(string FirstName, string LastName, string Email, string MobileNumber, bool Active) : ICommand<Result>;

internal sealed class CreateLeadCommandHandler : ICommandHandler<CreateLeadCommand, Result>
{
  private readonly ILeadService _service;

  public CreateLeadCommandHandler(ILeadService service) => _service = service;

  public ValueTask<Result> Handle(CreateLeadCommand command, CancellationToken cancellationToken)
  {
    var (FirstName, LastName, Email, MobileNumber, Active) = command;
    var aggregate = new Lead(FirstName, LastName, DateTime.Now, MobileNumber, Email);
    var payload = new Dictionary<string, object>
    {
      { "lead", JsonConvert.SerializeObject(aggregate) },
    };
    return _service.CreateALeadAsync(payload, cancellationToken);
  }
}
