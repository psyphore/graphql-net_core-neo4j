namespace ThumbezaTech.Leads.Domain.OrderAggregate.Events;

public sealed class OrderConfirmedEvent : BaseDomainEvent
{
  public Order Order { get; private set; }
  public OrderConfirmedEvent(Order order) => Order = order;
}

