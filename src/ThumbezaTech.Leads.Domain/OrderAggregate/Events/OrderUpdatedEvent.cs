namespace ThumbezaTech.Leads.Domain.OrderAggregate.Events;

public sealed class OrderUpdatedEvent : BaseDomainEvent
{
  public Order Order { get; private set; }
  public OrderUpdatedEvent(Order order) => Order = order;
}

