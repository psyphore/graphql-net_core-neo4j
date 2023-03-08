namespace ThumbezaTech.Leads.Domain.OrderAggregate.Events;
public sealed class OrderCreatedEvent : BaseDomainEvent
{
  public Order Order { get; private set; }
  public OrderCreatedEvent(Order order) => Order = order;
}


public sealed class OrderUpdatedEvent : BaseDomainEvent
{
  public Order Order { get; private set; }
  public OrderUpdatedEvent(Order order) => Order = order;
}


public sealed class OrderConfirmedEvent : BaseDomainEvent
{
  public Order Order { get; private set; }
  public OrderConfirmedEvent(Order order) => Order = order;
}

