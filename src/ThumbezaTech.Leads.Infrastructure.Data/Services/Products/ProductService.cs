using ThumbezaTech.Leads.Application.Products;
using ThumbezaTech.Leads.Domain.ProductAggregate;

namespace ThumbezaTech.Leads.Infrastructure.Data.Services.Products;

internal sealed class ProductService : IProductService
{
  private readonly INeo4jDataAccess _data;
  private readonly IRepository<Product> _repository;
  private const string Label = nameof(Product);

  public ProductService(IRepository<Product> repository, INeo4jDataAccess data)
    => (_repository, _data) = (repository, data);

  public async ValueTask<Result<IEnumerable<Product>>> QueryProducts(string query, CancellationToken cancellationToken = default)
  {
    Dictionary<string, object> Query = new()
    {
      { nameof(query), query },
    };
    var statement = Queries.Options[Queries.Search].Trim();
    var payload = await _data.ExecuteReadTransactionAsync<Product>(statement, $"{Label}s", Query);
    return payload.Any()
        ? Result.Success(payload.Distinct())
        : Result.NotFound();
  }

  public async ValueTask<Result<IEnumerable<Product>>> GetProducts(CancellationToken cancellationToken = default)
  {
    var statement = Queries.Options[Queries.GetAll].Trim();
    var payload = await _data.ExecuteReadTransactionAsync<Product>(statement, $"{Label}s", null!);
    return payload.Any()
        ? Result.Success(payload.Distinct())
        : Result.NotFound();
  }

  public async ValueTask<Result<Product>> GetProductById(string id, CancellationToken cancellationToken = default)
  {
    Dictionary<string, object> Query = new()
    {
      { nameof(id), id },
    };
    var statement = Queries.Options[Queries.GetOne].Trim();
    var payload = await _data.ExecuteReadTransactionAsync<Product>(statement, Label, Query);
    return payload is not null
        ? Result.Success(payload.First())
        : Result.NotFound();
  }

  public async ValueTask<Result> AddProduct(Product product, CancellationToken cancellationToken = default)
  {
    Dictionary<string, object> input = new()
    {
      { nameof(Product), product.Serialize() },
    };
    var statement = Commands.Options[Commands.SaveOne].Trim();
    var payload = await _data.ExecuteWriteTransactionAsync<string>(statement, input);
    return payload.Any()
        ? Result.SuccessWithMessage(payload)
        : Result.NotFound();
  }

  public async ValueTask<Result> UpdateProduct(Product product, CancellationToken cancellationToken = default)
  {
    Dictionary<string, object> input = new()
    {
      { nameof(Product) , product.Serialize() },
    };
    var statement = Commands.Options[Commands.UpdateOne].Trim();
    var payload = await _data.ExecuteWriteTransactionAsync<string>(statement, input);
    return payload.Any()
        ? Result.SuccessWithMessage(payload)
        : Result.NotFound();
  }
}
