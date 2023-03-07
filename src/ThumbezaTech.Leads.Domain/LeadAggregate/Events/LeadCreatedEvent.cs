namespace ThumbezaTech.Leads.Domain.LeadAggregate.Events;

public sealed class LeadCreatedEvent : BaseDomainEvent
{
    public Lead Lead { get; set; }
    public LeadCreatedEvent(Lead lead) => Lead = lead;
}

public sealed class LeadUpdatedEvent : BaseDomainEvent
{
    public Lead Lead { get; set; }
    public LeadUpdatedEvent(Lead lead) => Lead = lead;
}

public sealed class LeadDeletedEvent : BaseDomainEvent
{
    public Lead Lead { get; set; }
    public LeadDeletedEvent(Lead lead) => Lead = lead;
}

