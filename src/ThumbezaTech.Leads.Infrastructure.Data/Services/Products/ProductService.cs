using ThumbezaTech.Leads.Application.Products;
using ThumbezaTech.Leads.Domain.ProductAggregate;

namespace ThumbezaTech.Leads.Infrastructure.Data.Services.Products;

internal sealed class ProductService : IProductService
{
  private readonly IRepository<Product> _repository;
  private const string Label = nameof(Product);

  public ProductService(IRepository<Product> repository) => _repository = repository;

  public async ValueTask<Result<IEnumerable<Product>>> QueryProducts(IDictionary<string, object> Query, CancellationToken cancellationToken = default)
  {
    var statement = Queries.Options[Queries.Search].Trim();
    var records = await _repository.Read(statement, Query, cancellationToken);
    var payload = records.Where(record => record is not null).Select(record => record.ProcessRecords<Product>($"{Label}s"));

    return payload.Any()
        ? Result.Success(payload.Distinct())
        : Result.NotFound();
  }

  public async ValueTask<Result<IEnumerable<Product>>> GetProducts(IDictionary<string, object> Query, CancellationToken cancellationToken = default)
  {
    var statement = Queries.Options[Queries.GetAll].Trim();
    var records = await _repository.Read(statement, Query, cancellationToken);
    var payload = records.Where(record => record is not null).Select(record => record.ProcessRecords<Product>($"{Label}s"));

    return payload.Any()
        ? Result.Success(payload.Distinct())
        : Result.NotFound();
  }

  public async ValueTask<Result<Product>> GetProductById(IDictionary<string, object> Query, CancellationToken cancellationToken = default)
  {
    var statement = Queries.Options[Queries.GetOne].Trim();
    var records = await _repository.Read(statement, Query, cancellationToken);
    var payload = records.Where(record => record is not null).Select(record => record.ProcessRecords<Product>(Label));

    return payload is not null
        ? Result.Success(payload.First())
        : Result.NotFound();
  }

  public async ValueTask<Result> AddProduct(IDictionary<string, object> Query, CancellationToken cancellationToken = default)
  {
    var statement = Commands.Options[Commands.SaveOne].Trim();
    var records = await _repository.Write(statement, Query, cancellationToken);
    var payload = records.Select(record => record.ProcessRecords<Product>(Label));

    return payload.Any()
        ? Result.Success()
        : Result.NotFound();
  }

  public async ValueTask<Result> UpdateProduct(IDictionary<string, object> Query, CancellationToken cancellationToken = default)
  {
    var statement = Commands.Options[Commands.UpdateOne].Trim();
    var records = await _repository.Write(statement, Query, cancellationToken);
    var payload = records.Select(record => record.ProcessRecords<Product>(Label));

    return payload.Any()
        ? Result.Success()
        : Result.NotFound();
  }
}
