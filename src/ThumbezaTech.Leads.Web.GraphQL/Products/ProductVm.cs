using ThumbezaTech.Leads.Domain.ProductAggregate;

namespace ThumbezaTech.Leads.Web.GraphQL.Products;

public sealed record ProductVm
{
  public string Id { get; set; } = default!;
  public string Name { get; set; } = default!;
  public Money? Price { get; set; } = default!;
  public List<string> Tags { get; set; } = new();
  public string Sku { get; set; } = default!;

  public static explicit operator ProductVm(Product v) => new()
  {
    Id = v.Id,
    Name = v.Name,
    Price = v.Price,
    Tags = v.Tags.ToList(),
    Sku = v.Sku.Value
  };

  public static implicit operator Product(ProductVm p)
    => new(p.Name, p.Price, p.Sku, p.Tags)
    {
      Id = p.Id
    };
}
