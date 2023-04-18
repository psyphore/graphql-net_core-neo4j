namespace ThumbezaTech.Leads.Domain.LeadAggregate.Events;

public sealed class LeadUpdatedEvent : BaseDomainEvent
{
  public Lead Lead { get; set; }
  public LeadUpdatedEvent(Lead lead) => Lead = lead;
}
