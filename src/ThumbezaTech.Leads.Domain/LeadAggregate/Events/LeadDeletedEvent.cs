namespace ThumbezaTech.Leads.Domain.LeadAggregate.Events;

public sealed class LeadDeletedEvent : BaseDomainEvent
{
  public Lead Lead { get; set; }
  public LeadDeletedEvent(Lead lead) => Lead = lead;
}

