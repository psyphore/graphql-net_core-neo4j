namespace ThumbezaTech.Leads.Domain.LeadAggregate.Events;

public sealed class LeadCreatedEvent : BaseDomainEvent
{
  public Lead Lead { get; set; }
  public LeadCreatedEvent(Lead lead) => Lead = lead;
}
