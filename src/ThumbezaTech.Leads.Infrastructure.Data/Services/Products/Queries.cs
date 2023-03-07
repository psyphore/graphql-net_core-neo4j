namespace ThumbezaTech.Leads.Infrastructure.Data.Services.Products;

public static class Queries
{
    public static readonly string GetOne = nameof(GetOne);
    public static readonly string GetAll = nameof(GetAll);
    public static readonly string Search = nameof(Search);

    public static Dictionary<string, string> Options => new()
    {
        { GetOne, @"
                    MATCH (l:Lead{id:$id}) 
                    RETURN l { 
                      .*
                      packages: [(cover)-[:COVER_OF]->(covered_packages:Package) | covered_packages]
                    } AS lead
                " },
        { GetAll, @"
                    MATCH (l:Lead) 
                    RETURN l { 
                      .*
                      packages: [(cover)-[:COVER_OF]->(covered_packages:Package) | covered_packages]
                    } AS lead
                    ORDER BY lead.LastName ASC
                    SKIP $offset
                    LIMIT $first
                " },
        { Search, @"
                    OPTIONAL MATCH (p:Product)
                    WHERE p.Name =~ $query 
                        OR p.Description =~ $query 
                        OR p.Value =~ $query
                    RETURN p AS product
                    ORDER BY p.Value DESC
                " },
    };
}
