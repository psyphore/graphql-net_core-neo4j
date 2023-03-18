using ThumbezaTech.Leads.Domain.ProductAggregate;

namespace ThumbezaTech.Leads.Application.Products;

public interface IProductService
{
  ValueTask<Result<IEnumerable<Product>>> QueryProducts(string query, CancellationToken cancellationToken = default);
  ValueTask<Result> AddProduct(Product product, CancellationToken cancellationToken = default);
  ValueTask<Result> UpdateProduct(Product product, CancellationToken cancellationToken = default);
  ValueTask<Result<Product>> GetProductById(string id, CancellationToken cancellationToken = default);
  ValueTask<Result<IEnumerable<Product>>> GetProducts(CancellationToken cancellationToken = default);
}
