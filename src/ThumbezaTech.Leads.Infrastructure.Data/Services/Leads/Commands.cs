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
            , apoc.json.path($Lead, '$.Addresses') AS addresses
            , apoc.json.path($Lead, '$.Contacts') AS contacts
            , timestamp() AS createdOn
            CALL {
              WITH addresses, createdOn
              UNWIND addresses AS address
              CREATE (a:Address {created: createdOn, active: true})
              SET a += address {
                  line1: address.Line1,
                  line2: address.Line2,
                  line3: address.Line3,
                  suburb: address.Suburb,
                  zip: address.Zip,
                  country: address.Country
              }
              RETURN a
            }
            CALL {
              WITH lead, createdOn
              MERGE (l:Lead {id: apoc.create.uuid(), created: createdOn, active: false})
              ON CREATE SET l += lead { 
                  firstName: lead.FirstName,
                  lastName: lead.LastName,
                  dateOfBirth: lead.DateOfBirth
              }
              RETURN l
            }
            CALL {
                WITH contacts, createdOn
                UNWIND contacts AS contact
                CREATE (c:Contact {created: createdOn, active: true})
                SET c += contact {
                  number: contact.Number,
                  email: contact.Email
                }
                RETURN c
            }
            CALL {
                WITH l, a, c, createdOn
                CREATE 
                  (l)-[r1:RESIDES_AT{ created: createdOn }]->(a),
                  (l)-[r2:HAS_CONTACT{ created: createdOn }]->(c)
                RETURN r1, r2
            }
            RETURN l.id AS Lead
        "},
        { UpdateOne, @"
          WITH apoc.json.path($Lead) AS lead
          , apoc.json.path($Lead, '$.Addresses') AS addresses
          , apoc.json.path($Lead, '$.Contacts') AS contacts
          , timestamp() AS updatedOn
          CALL {
            WITH lead, updatedOn
            MERGE (l:Lead { id: lead.Id })
            ON MATCH SET l += lead { 
                fisrtName: lead.FirstName,
                lastName: lead.LastName,
                dateOfBirth: lead.DateOfBirth,
                updated: updatedOn
            }
            ON CREATE SET l += lead { 
                fisrtName: lead.FirstName,
                lastName: lead.LastName,
                dateOfBirth: lead.DateOfBirth,
                updated: updatedOn
            }
            RETURN l
          }
          CALL {
            WITH addresses, updatedOn
            UNWIND addresses AS address
            MERGE (a:Address {id: address.Id})
            ON CREATE SET a += address {
                line1: address.Line1,
                line2: address.Line2,
                line3: address.Line3,
                suburb: address.Suburb,
                zip: address.Zip,
                country: address.Country,
                created: updatedOn
            }
            ON MATCH SET a += address {
                line1: address.Line1,
                line2: address.Line2,
                line3: address.Line3,
                suburb: address.Suburb,
                zip: address.Zip,
                country: address.Country,
                updated: updatedOn
            }
            RETURN a
          }
          CALL {
            WITH contacts, updatedOn
            UNWIND contacts AS contact
            MERGE (c:Contact {id: contact.Id})
            ON MATCH SET c += contact {
              number: contact.Number,
              email: contact.Email,
              updated: updatedOn
            }
            ON CREATE SET c += contact {
              number: contact.Number,
              email: contact.Email,
              created: updatedOn
            }
            RETURN c
          }
          RETURN l.id AS Lead
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
