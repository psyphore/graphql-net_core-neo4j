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
      OPTIONAL MATCH (l:Lead{Id: $id})
      CALL {
          WITH l
          OPTIONAL MATCH (l)--(c:Contact)
          UNWIND c.Number AS numbers
          RETURN numbers
      }
      CALL{
          WITH l
          OPTIONAL MATCH (l)--(a:Address)
          //UNWIND a AS addresses
          RETURN a AS addresses
      }
      RETURN l {
          id: l.Id,
          firstName: l.FirstName,
          lastName: l.LastName,
          dateOfBirth: l.DateOfBirth,
          emailAddress: l.EmailAddress,
          address: addresses { .* },
          numbers: numbers
      } AS Lead
      "},
    {
      GetAll, @"
      MATCH (l:Lead)
      CALL {
          WITH l
          OPTIONAL MATCH (l)--(c:Contact)
          UNWIND c.Number AS numbers
          RETURN numbers
      }
      CALL{
          WITH l
          OPTIONAL MATCH (l)--(a:Address)
          //UNWIND a AS addresses
          RETURN a AS addresses
      }
      RETURN l {
          id: l.Id,
          firstName: l.FirstName,
          lastName: l.LastName,
          dateOfBirth: l.DateOfBirth,
          emailAddress: l.EmailAddress,
          address: addresses { .* },
          numbers: numbers
      } AS Lead
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
