namespace ThumbezaTech.Leads.Infrastructure.Data.Services.Leads;

internal static class Commands
{
  public static readonly string SaveOne = nameof(SaveOne);
  public static readonly string UpdateOne = nameof(UpdateOne);

  public static Dictionary<string, string> Options => new()
    {
        { SaveOne, @"
            WITH apoc.json.path($Lead) AS lead
            , apoc.json.path($Lead, '$.Address') AS address
            , apoc.json.path($Lead, '$.Address.ContactNumbers..$values') AS contacts
            , timestamp() AS createdOn
            CALL {
              WITH address, createdOn
              CREATE (a:Address {created: createdOn})
              SET a += address {
                  .Line1,
                  .Line2,
                  .Line3,
                  .Suburb,
                  .Zip,
                  .Country
              }
              RETURN a
            }
            CALL {
              WITH lead, createdOn
              MERGE (l:Lead{ id: apoc.create.uuid(), created: createdOn })
              ON CREATE SET l += lead { 
                  .FirstName,
                  .LastName,
                  .DateOfBirth,
                  .MobileNumber,
                  .EmailAddress
              }
              RETURN l
            }
            CALL {
                WITH contacts, createdOn
                UNWIND contacts AS contact
                CREATE (c:Contact { id: apoc.create.uuid(), created: createdOn, number: contact})
                RETURN c
            }
            CALL {
                WITH l, a, c, createdOn
                CREATE (l)-[r1:RESIDES_AT{created: createdOn}]->(a)<-[r2:HAS_CONTACT]-(c)
                RETURN r1, r2
            }
            RETURN r1, r2, l AS Lead
        "},
        { UpdateOne, @"
          WITH apoc.json.path($lead) AS lead
          CALL {
            WITH lead
            MERGE (l:Lead { id: Lead.id })
            ON MATCH SET l += lead
            RETURN l
          }
          RETURN l AS Lead
        "}
    };
}
