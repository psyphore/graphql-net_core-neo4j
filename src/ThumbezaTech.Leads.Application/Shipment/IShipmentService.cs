using ThumbezaTech.Leads.Domain.ShipmentAggregate;

namespace ThumbezaTech.Leads.Application.Shipments;

public interface IShipmentService
{
  ValueTask<Result<List<Shipment>>> GetShipments();
  ValueTask<Result<List<Shipment>>> GetShipmentsForLead(string id);
  ValueTask<Result<Shipment>> GetShipment(string id);
  ValueTask<Result> CreateShipment(Shipment shipment);
  ValueTask<Result> UpdateShipment(Shipment shipment);

}
