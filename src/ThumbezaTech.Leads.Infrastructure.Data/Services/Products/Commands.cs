namespace ThumbezaTech.Leads.Infrastructure.Data.Services.Products;

public static class Commands
{
  public static readonly string SaveOne = nameof(SaveOne);
  public static readonly string UpdateOne = nameof(UpdateOne);

  public static Dictionary<string, string> Options => new()
    {
        { SaveOne, @"
                    MATCH (p:Product{id:$id}) 
                    RETURN p AS product
                " },
        { UpdateOne, @"
                    MATCH (p:Product) 
                    RETURN p AS products
                    ORDER BY p.value ASC
                    SKIP $offset
                    LIMIT $first
                " }
    };
}
