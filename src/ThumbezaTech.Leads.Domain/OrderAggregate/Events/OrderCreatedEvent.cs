namespace ThumbezaTech.Leads.Domain.OrderAggregate.Events;
public sealed class OrderCreatedEvent : BaseDomainEvent
{
  public Order Order { get; private set; }
  public OrderCreatedEvent(Order order) => Order = order;
}

