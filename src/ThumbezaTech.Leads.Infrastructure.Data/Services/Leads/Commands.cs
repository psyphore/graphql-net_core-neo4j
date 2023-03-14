namespace ThumbezaTech.Leads.Infrastructure.Data.Services.Leads;

internal static class Commands
{
  public static readonly string SaveOne = nameof(SaveOne);
  public static readonly string UpdateOne = nameof(UpdateOne);
  public static readonly string UpdateLeadAddress = nameof(UpdateLeadAddress);

  public static Dictionary<string, string> Options => new()
    {
        { SaveOne, @"
            WITH apoc.json.path($Lead) AS lead
            , apoc.json.path($Lead, '$.Address') AS address
            , apoc.json.path($Lead, '$.Address.ContactNumbers..$values') AS contacts
            , timestamp() AS createdOn
            CALL {
              WITH address, createdOn
              CREATE (a:Address {Created: createdOn})
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
              MERGE (l:Lead{ Id: apoc.create.uuid(), Created: createdOn, Active: false })
              ON CREATE SET l += lead { 
                  .FirstName,
                  .LastName,
                  .DateOfBirth,
                  .EmailAddress
              }
              RETURN l
            }
            CALL {
                WITH contacts, createdOn
                UNWIND contacts AS contact
                CREATE (c:Contact { Id: apoc.create.uuid(), Created: createdOn, Number: contact})
                RETURN c
            }
            CALL {
                WITH l, a, c, createdOn
                CREATE (l)-[r1:RESIDES_AT{ Created: createdOn }]->(a)<-[r2:HAS_CONTACT{ Created: createdOn }]-(c)
                RETURN r1, r2
            }
            RETURN l.Id AS Lead
        "},
        { UpdateOne, @"
          WITH apoc.json.path($Lead) AS lead
          , apoc.json.path($Lead, '$.Address') AS address
          , apoc.json.path($Lead, '$.Address.ContactNumbers..$values') AS contacts
          , timestamp() AS updatedOn
          CALL {
            WITH lead
            MERGE (l:Lead { Id: Lead.id })
            ON MATCH SET l += lead { 
                .FirstName,
                .LastName,
                .DateOfBirth,
                .EmailAddress,
                Updated: updatedOn
            }
            RETURN l
          }
          RETURN l.Id AS Lead
        "},
        { UpdateLeadAddress, @"
          WITH apoc.json.path($Lead) AS lead
          , apoc.json.path($Lead, '$.Address') AS address
          , apoc.json.path($Lead, '$.Address.ContactNumbers..$values') AS contacts
          , timestamp() AS updatedOn
          CALL {
            WITH lead
            MERGE (l:Lead { Id: Lead.id })
            ON MATCH SET l += lead { 
                .FirstName,
                .LastName,
                .DateOfBirth,
                .EmailAddress,
                Updated: updatedOn
            }
            RETURN l
          }
          RETURN l.Id AS Lead
        "}
    };
}
