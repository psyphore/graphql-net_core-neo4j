namespace ThumbezaTech.Leads.Domain.ShipmentAggregate.Events;
public sealed class ShipmentCreatedEvent : BaseDomainEvent
{
  public ShipmentCreatedEvent(Shipment shipment) => Shipment = shipment;
  public Shipment Shipment { get; }
}
