using ThumbezaTech.Leads.Domain.ProductAggregate;

namespace ThumbezaTech.Leads.Application.Products;

public sealed record GetProductsQuery(string Query, int Page = 1, int Size = 10) : IQuery<Result<IEnumerable<Product>>>;

internal sealed class GetProductQueryHandler : IQueryHandler<GetProductsQuery, Result<IEnumerable<Product>>>
{
    private readonly IProductService _searchRepository;

    public GetProductQueryHandler(IProductService searchRepository) => _searchRepository = searchRepository;

    public ValueTask<Result<IEnumerable<Product>>> Handle(GetProductsQuery query, CancellationToken cancellationToken)
    {
        var (Query, Size, Page) = query;
        var parameter = new Dictionary<string, object>
        {
            { "offset", Page },
            { "limit", Size },
            { "query", Query },
        };
        return _searchRepository.QueryProducts(parameter, cancellationToken);
    }
}
