namespace ThumbezaTech.Leads.Domain.LeadAggregate.Events;

public sealed class LeadAbandonedEvent : BaseDomainEvent
{
  public LeadAbandonedEvent(string leadId) => LeadId = leadId;

  public string LeadId { get; }
}
