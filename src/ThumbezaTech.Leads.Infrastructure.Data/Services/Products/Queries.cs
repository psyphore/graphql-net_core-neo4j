namespace ThumbezaTech.Leads.Infrastructure.Data.Services.Products;

public static class Queries
{
    public static readonly string GetOne = nameof(GetOne);
    public static readonly string GetAll = nameof(GetAll);
    public static readonly string Search = nameof(Search);

    public static Dictionary<string, string> Options => new()
    {
        { GetOne, @"
                    MATCH (p:Product{id:$id}) 
                    RETURN p AS product
                " },
        { GetAll, @"
                    MATCH (p:Product) 
                    RETURN p AS products
                    ORDER BY p.value ASC
                    SKIP $offset
                    LIMIT $first
                " },
        { Search, @"
                    OPTIONAL MATCH (p:Product)
                    WHERE (p.value <= toFloat($query) OR toFloat($query) >= p.value)
                        OR p.name =~ $query
                        OR p.tags IN [$query]
                    WITH p
                    RETURN p
                    ORDER BY p.value ASC
                    SKIP $offset
                    LIMIT $first
                " },
    };
}
