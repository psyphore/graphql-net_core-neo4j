namespace ThumbezaTech.Leads.Infrastructure.Data.Services.Leads;

internal static class Queries
{
  public static readonly string GetOne = nameof(GetOne);
  public static readonly string GetAll = nameof(GetAll);
  public static readonly string Search = nameof(Search);

  public static Dictionary<string, string> Options => new()
  {
    {
      GetOne, @"
      OPTIONAL MATCH (l:Lead{ id: $id })
      RETURN l AS Lead
      "},
    {
      GetAll, @"
      MATCH (l:Lead)
      RETURN l AS Lead
      ORDER BY Lead.lastName ASC, Lead.firstName ASC
      "},
    {
      Search, @"
      OPTIONAL MATCH (l:Lead)
      WHERE l.firstName CONTAINS $query
          OR l.lastName CONTAINS $query
          OR l.email CONTAINS $query
          OR l.mobileNumber CONTAINS $query
      RETURN l AS Lead
      ORDER BY Lead.lastName ASC, Lead.firstName ASC
      "}
  };
}
