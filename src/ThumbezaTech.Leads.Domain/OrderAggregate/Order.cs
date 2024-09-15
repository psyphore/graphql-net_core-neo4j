using ThumbezaTech.Leads.Domain.LeadAggregate;
using ThumbezaTech.Leads.Domain.ProductAggregate;
using ThumbezaTech.Leads.SharedKernel.Interfaces;

namespace ThumbezaTech.Leads.Domain.OrderAggregate;
public sealed class Order : BaseEntity, IAggregateRoot
{
  internal Order() { }

  public readonly HashSet<LineItem> LineItems = new();
  public new string Id { get; private set; }
  public Lead? Customer { get; private set; }

  public static Order Create(Lead? customer)
  {
    Order order = new()
    {
      Id = Guid.NewGuid().ToString(),
      Customer = customer ?? default!,
    };
    return order;
  }

  public void AddLineItem(Product product, Money value)
  {
    LineItem lineItem = new(Guid.NewGuid().ToString(), Id, product.Id, value.Currency, value.Amount);
    LineItems.Add(lineItem);
  }
}
