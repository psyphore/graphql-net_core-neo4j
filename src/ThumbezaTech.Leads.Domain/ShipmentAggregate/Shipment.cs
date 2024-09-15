using ThumbezaTech.Leads.Domain.AddressValueObject;
using ThumbezaTech.Leads.Domain.OrderAggregate;
using ThumbezaTech.Leads.SharedKernel.Interfaces;

namespace ThumbezaTech.Leads.Domain.ShipmentAggregate;

public sealed class Shipment : BaseEntity, IAggregateRoot
{
  internal Shipment() { }

  public Shipment(string id, Order orderToShip, Address deliverTo)
  {
    Id = id;
    OrderToShip = orderToShip;
    DeliverTo = deliverTo;
  }

  public Order OrderToShip { get; }
  public Address DeliverTo { get; }
}
