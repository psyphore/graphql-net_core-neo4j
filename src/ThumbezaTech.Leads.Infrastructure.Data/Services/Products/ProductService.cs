using ThumbezaTech.Leads.Application.Products;
using ThumbezaTech.Leads.Domain.ProductAggregate;

namespace ThumbezaTech.Leads.Infrastructure.Data.Services.Products;

public class ProductService : IProductService
{
    private readonly IRepository<Product> _repository;
    private const string Label = nameof(Product);

    public ProductService(IRepository<Product> repository) => _repository = repository;

    public async ValueTask<Result<IEnumerable<Product>>> QueryProducts(IDictionary<string, object> Query, CancellationToken cancellationToken = default)
    {
        var statement = Queries.Options[Queries.Search].Trim();
        var records = await _repository.Write(statement, Query, cancellationToken);
        var payload = records.Select(record => record.ProcessRecords<Product>(Label));

        return payload.Any()
            ? Result.Success(payload.Distinct())
            : Result.NotFound();
    }
}
