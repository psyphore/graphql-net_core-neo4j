using ThumbezaTech.Leads.Domain.LeadAggregate;
using ThumbezaTech.Leads.Domain.ProductAggregate;
using ThumbezaTech.Leads.SharedKernel.Interfaces;

namespace ThumbezaTech.Leads.Domain.OrderAggregate;
public sealed class Order : BaseEntity, IAggregateRoot
{
  private Order() { }

  private readonly HashSet<LineItem> _lineItems = new();
  public new string Id { get; private set; }
  public string CustomerId { get; private set; }

  public static Order Create(Lead customer)
  {
    Order order = new()
    {
      Id = Guid.NewGuid().ToString(),
      CustomerId = customer.Id,
    };
    return order;
  }

  public void Add(Product product)
  {
    LineItem lineItem = new(Guid.NewGuid().ToString(), Id, product.Id, product.Price);
    _lineItems.Add(lineItem);
  }
}
