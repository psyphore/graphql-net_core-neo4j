namespace ThumbezaTech.Leads.Infrastructure.Data.Services.Orders;

internal static class Queries
{
  public static readonly string GetOne = nameof(GetOne);
  public static readonly string GetAll = nameof(GetAll);
  public static readonly string Search = nameof(Search);
  public static readonly string LeadOrders = nameof(LeadOrders);

  public static Dictionary<string, string> Options => new()
  {
    {
      GetOne, @"
      OPTIONAL MATCH (o:Order{ id: $id })
      RETURN o AS Order
      "},
    {
      GetAll, @"
      MATCH (o:Orders)
      RETURN o AS Orders
      "},
    {
      LeadOrders, @"
      OPTIONAL MATCH(Lead{id: $id})<--(o:Order)
      RETURN o AS Orders
      "}
  };
}
