namespace ThumbezaTech.Leads.Infrastructure.Data.Services.Products;

internal static class Queries
{
  public static readonly string GetOne = nameof(GetOne);
  public static readonly string GetAll = nameof(GetAll);
  public static readonly string Search = nameof(Search);

  public static Dictionary<string, string> Options => new()
    {
        { GetOne, @"
          OPTIONAL MATCH (p:Product{ id: $id })<--(m:Money{ active: true })
          WITH p { 
                .id,
                .name,
                .sku,
                tags: [(p)<--(t:Tag) | t.value], 
                currency: m.currency,
                amount: m.amount  
                } AS Product
          RETURN Product
        "},
        { GetAll, @"
          MATCH (p:Product)<--(m:Money{ active: true }) 
          RETURN p { 
                .id,
                .name,
                .sku,
                tags: [(p)<--(t:Tag) | t.value], 
                currency: m.currency,
                amount: m.amount  
                } AS Products
          ORDER BY p.name ASC, m.amount ASC
        "},
        { Search, @"
          OPTIONAL MATCH (p:Product)<--(m:Money{ active: true })
          WHERE (m.amount <= toFloat($query) AND m.amount >= toFloat($query))
            OR p.name =~ '(?i)$query*'
            OR p.name CONTAINS $query
          RETURN p { 
                .id,
                .name,
                .sku,
                tags: [(p)<--(t:Tag) | t.value], 
                currency: m.currency,
                amount: m.amount  
                } AS Products
          ORDER BY p.name ASC, m.amount ASC
        "},
    };
}
