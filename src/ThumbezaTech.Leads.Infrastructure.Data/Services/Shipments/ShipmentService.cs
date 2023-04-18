using ThumbezaTech.Leads.Application.Shipments;
using ThumbezaTech.Leads.Domain.ShipmentAggregate;

namespace ThumbezaTech.Leads.Infrastructure.Data.Services.Products;

internal sealed class ShipmentService : IShipmentService
{
  private readonly INeo4jDataAccess _data;
  private const string Label = nameof(Shipment);

  public ShipmentService(INeo4jDataAccess data) => _data = data;

  public async ValueTask<Result<List<Shipment>>> GetShipments()
  {
    var statement = Queries.Options[Queries.GetAll].Trim();
    var payload = await _data.ExecuteReadTransactionAsync<Shipment>(statement, $"{Label}s", null!);
    return payload.Any()
        ? Result.Success(payload)
        : Result.NotFound();
  }

  public async ValueTask<Result<List<Shipment>>> GetShipmentsForLead(string id)
  {
    Dictionary<string, object> Query = new()
    {
      { nameof(id), id },
    };
    var statement = Queries.Options[Queries.GetOne].Trim();
    var payload = await _data.ExecuteReadTransactionAsync<Shipment>(statement, $"{Label}s", Query);
    return payload is not null
        ? Result.Success(payload)
        : Result.NotFound();
  }

  public async ValueTask<Result<Shipment>> GetShipment(string id)
  {
    Dictionary<string, object> Query = new()
    {
      { nameof(id), id },
    };
    var statement = Queries.Options[Queries.GetOne].Trim();
    var payload = await _data.ExecuteReadTransactionAsync<Shipment>(statement, Label, Query);
    return payload is not null
        ? Result.Success(payload.First())
        : Result.NotFound();
  }

  public async ValueTask<Result> CreateShipment(Shipment shipment)
  {
    Dictionary<string, object> input = new()
    {
      { nameof(Shipment) , shipment.Serialize() },
    };
    var statement = Commands.Options[Commands.SaveOne].Trim();
    var payload = await _data.ExecuteWriteTransactionAsync<string>(statement, input);
    return payload.Any()
        ? Result.SuccessWithMessage(payload)
        : Result.NotFound();
  }

  public async ValueTask<Result> UpdateShipment(Shipment shipment)
  {
    Dictionary<string, object> input = new()
    {
      { nameof(Shipment), shipment.Serialize() },
    };
    var statement = Commands.Options[Commands.UpdateOne].Trim();
    var payload = await _data.ExecuteWriteTransactionAsync<string>(statement, input);
    return payload.Any()
        ? Result.SuccessWithMessage(payload)
        : Result.NotFound();
  }
}
