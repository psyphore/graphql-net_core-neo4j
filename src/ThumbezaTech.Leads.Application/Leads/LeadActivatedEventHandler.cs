using ThumbezaTech.Leads.Domain.LeadAggregate.Events;

namespace ThumbezaTech.Leads.Application.Leads;

internal sealed class LeadActivatedEventHandler : INotificationHandler<LeadActivedEvent>
{
  private readonly ILeadService _service;
  public LeadActivatedEventHandler(ILeadService service) => _service = service;

  public ValueTask Handle(LeadActivedEvent notification, CancellationToken cancellationToken)
  {
    throw new NotImplementedException();
  }
}
