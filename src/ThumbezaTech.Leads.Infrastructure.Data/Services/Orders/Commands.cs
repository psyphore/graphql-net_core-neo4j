namespace ThumbezaTech.Leads.Infrastructure.Data.Services.Orders;

internal static class Commands
{
  public static readonly string SaveOne = nameof(SaveOne);
  public static readonly string UpdateOne = nameof(UpdateOne);

  public static Dictionary<string, string> Options => new()
    {
        { SaveOne, @"
            WITH apoc.json.path($lineItems) AS lineItems,
                 apoc.json.path($customer) AS customer,
                 timestamp() AS createdOn
            CALL {
              WITH createdOn
              CREATE (o:Order{ id: apoc.create.uuid(), created: createdOn })
              RETURN o
            }
            CALL {
              WITH lineItems, o, createdOn
              FOREACH(li IN lineItems | CREATE(:LineItem{ 
                id: apoc.create.uuid(), 
                created: createdOn,
              })-[:HAS_ITEM { created: createdOn }]->(o))
              RETURN COUNT(lineItems) AS linked
            }
            CALL {
              WITH customer, createdOn
              MERGE (lead:Lead { id: customer.id })
              ON CREATE SET lead.created = createdOn,
                            lead += customer
              RETURN lead
            }
            CALL {
              WITH o, lead, createdOn
              CREATE (lead)-[r2:HAS_ORDER {created: createdOn}]->(o)
              RETURN lead, o, r2
            }
            RETURN o.id AS Order
        "},
        { UpdateOne, @"
          WITH apoc.json.path($order) AS _order
          CALL {
            WITH _order
            MERGE (o:Order { id: _order.id })
            ON MATCH SET o += _order
            RETURN o
          }
          RETURN o.id AS Order
        "}
    };
}
