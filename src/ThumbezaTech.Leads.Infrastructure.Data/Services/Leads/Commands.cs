namespace ThumbezaTech.Leads.Infrastructure.Data.Services.Leads;

internal static class Commands
{
  public static readonly string SaveOne = nameof(SaveOne);
  public static readonly string UpdateOne = nameof(UpdateOne);

  public static Dictionary<string, string> Options => new()
    {
        { SaveOne, @"
            WITH apoc.json.path($lead) AS lead
            CALL {
              WITH lead
              MERGE (l:Lead{ id: apoc.create.uuid() })
              ON CREATE SET l.created = timestamp()
              SET l += lead
              RETURN l
            }
            RETURN l AS lead
        "},
        { UpdateOne, @"
          WITH apoc.json.path($lead) AS lead
          CALL {
            WITH lead
            MATCH (l:Lead { id: lead.id })
            ON MATCH SET l += lead
            RETURN l
          }
          RETURN l AS lead
        "}
    };
}
