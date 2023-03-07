using ThumbezaTech.Leads.Domain.ProductAggregate;

namespace ThumbezaTech.Leads.Application.Products;

public interface IProductService
{
    ValueTask<Result<IEnumerable<Product>>> QueryProducts(IDictionary<string, object> Query, CancellationToken cancellationToken = default);
}