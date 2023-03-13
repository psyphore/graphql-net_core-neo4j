namespace ThumbezaTech.Leads.Infrastructure.Data.Services.Products;

internal static class Commands
{
  public static readonly string SaveOne = nameof(SaveOne);
  public static readonly string UpdateOne = nameof(UpdateOne);

  public static Dictionary<string, string> Options => new()
    {
        { 
          SaveOne, @"
          WITH apoc.json.path($product) AS product
          CALL {
            WITH product
            MERGE (p:Product{id: apoc.create.uuid()})
            ON CREATE SET p.created = timestamp(), p += product
            RETURN p
          }
          RETURN p AS Product
        "},
        { 
          UpdateOne, @"
          WITH apoc.json.path($product) AS product
          CALL {
            WITH product
            MATCH (p:Product { id: product.id })
            ON MATCH SET p += product
            RETURN p
          }
          RETURN p AS product
        "}
    };
}
