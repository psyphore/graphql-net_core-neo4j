namespace ThumbezaTech.Leads.Domain.LeadAggregate.Events;

public sealed class LeadActivedEvent : BaseDomainEvent
{
  public LeadActivedEvent(string leadId) => LeadId = leadId;

  public string LeadId { get; }
}
