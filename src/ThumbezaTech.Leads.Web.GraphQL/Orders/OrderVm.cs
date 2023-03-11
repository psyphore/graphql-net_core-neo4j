using ThumbezaTech.Leads.Domain.ProductAggregate;

namespace ThumbezaTech.Leads.Web.GraphQL.Orders;

public sealed record OrderVm
{
  public string Id { get; set; }
  public string CustomerId { get; set; }
  public IReadOnlyCollection<LineItemVm> LineItems { get; set; }
}

public sealed record LineItemVm
{
  public string Id { get; set; }
  public string ProductId { get; set; }
  public Money Value { get; set; }
}
