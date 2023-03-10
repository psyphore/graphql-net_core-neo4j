using ThumbezaTech.Leads.Domain.ProductAggregate;

namespace ThumbezaTech.Leads.Application.Products;

public interface IProductService
{
  ValueTask<Result<IEnumerable<Product>>> QueryProducts(IDictionary<string, object> Query, CancellationToken cancellationToken = default);
  ValueTask<Result> AddProduct(IDictionary<string, object> Query, CancellationToken cancellationToken = default);
  ValueTask<Result> UpdateProduct(IDictionary<string, object> Query, CancellationToken cancellationToken = default);
  ValueTask<Result<Product>> GetProductById(IDictionary<string, object> Query, CancellationToken cancellationToken = default);
  ValueTask<Result<IEnumerable<Product>>> GetProducts(IDictionary<string, object> Query, CancellationToken cancellationToken = default);
}
