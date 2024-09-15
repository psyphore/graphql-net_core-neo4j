namespace ThumbezaTech.Leads.Domain.ShipmentAggregate.Events;

public sealed class ShipmentDeliveredEvent : BaseDomainEvent
{
  public ShipmentDeliveredEvent(Shipment shipment) => Shipment = shipment;
  public Shipment Shipment { get; }
}


