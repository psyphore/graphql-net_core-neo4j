using ThumbezaTech.Leads.Domain.ProductAggregate;

namespace ThumbezaTech.Leads.Domain.OrderAggregate;

public sealed class LineItem : BaseEntity
{
  public new string Id { get; private set; }
  public string OrderId { get; private set; }
  public string ProductId { get; private set; }
  public Money Price { get; private set; }

  internal LineItem(string id, string orderId, string productId, Money price)
  {
    Id = id;
    OrderId = orderId;
    ProductId = productId;
    Price = price;
  }
}
