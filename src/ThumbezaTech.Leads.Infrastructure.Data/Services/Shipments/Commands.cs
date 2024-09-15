namespace ThumbezaTech.Leads.Infrastructure.Data.Services.Shipments;

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
              CREATE (s:Shipment{ id: apoc.create.uuid(), created: createdOn })
              RETURN s
            }
            CALL {
              WITH lineItems, s, createdOn
              FOREACH(li IN lineItems | CREATE(:LineItem { 
                id: apoc.create.uuid(), 
                created: createdOn,
              })-[:HAS_ITEM { created: createdOn }]->(s))
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
              WITH s, lead, createdOn
              CREATE (lead)-[r2:HAS_SHIPMENT {created: createdOn}]->(s)
              RETURN lead, s, r2
            }
            RETURN s.id AS Shipment
        "},
        { UpdateOne, @"
          WITH apoc.json.path($shipment) AS _shipment
          CALL {
            WITH _shipment
            MERGE (s:Shipment { id: _shipment.id })
            ON MATCH SET s += _shipment
            RETURN s
          }
          RETURN s.id AS Shipment
        "}
    };
}
