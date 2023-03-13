namespace ThumbezaTech.Leads.Domain.ShipmentAggregate.Events;

public sealed class ShipmentReturnedEvent : BaseDomainEvent
{
  public ShipmentReturnedEvent(Shipment shipment) => Shipment = shipment;
  public Shipment Shipment { get; }
}


