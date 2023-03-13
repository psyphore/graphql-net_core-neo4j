namespace ThumbezaTech.Leads.Domain.ShipmentAggregate.Events;

public sealed class ShipmentUpdatedEvent : BaseDomainEvent
{
  public ShipmentUpdatedEvent(Shipment shipment) => Shipment = shipment;
  public Shipment Shipment { get; }
}


