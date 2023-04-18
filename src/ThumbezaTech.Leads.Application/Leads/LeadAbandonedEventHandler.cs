using ThumbezaTech.Leads.Domain.LeadAggregate.Events;

namespace ThumbezaTech.Leads.Application.Leads;

internal sealed class LeadAbandonedEventHandler : INotificationHandler<LeadAbandonedEvent>
{
  private readonly ILeadService _service;
  public LeadAbandonedEventHandler(ILeadService service) => _service = service;

  public ValueTask Handle(LeadAbandonedEvent notification, CancellationToken cancellationToken)
  {
    throw new NotImplementedException();
  }
}
