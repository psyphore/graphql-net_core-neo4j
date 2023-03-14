using Ardalis.GuardClauses;

using Newtonsoft.Json;

using ThumbezaTech.Leads.SharedKernel.Interfaces;

namespace ThumbezaTech.Leads.Domain.ProductAggregate;

public sealed class Product : BaseEntity, IAggregateRoot
{
  public string Name { get; private set; } = default!;

  public Money Price { get; private set; } = default!;

  public IReadOnlySet<string> Tags { get; private set; } //=> _tags.ToList().AsReadOnly();

  public Sku Sku { get; private set; } = default!;

  internal Product() { }

  public Product(string name, Money price, string sku, IEnumerable<string> tags)
  {
    Name = Guard.Against.NullOrEmpty(name, nameof(name));
    Price = Guard.Against.Null(price, nameof(price));
    Tags = Guard.Against.NullOrInvalidInput(tags, nameof(tags), items => items.Count() != 0).ToHashSet();
    Sku = Sku.Create(sku);

  }

  [JsonConstructor]
  internal Product(string id, string name, string currency, decimal amount, string sku, IEnumerable<string> tags)
  {
    Id = Guard.Against.NullOrEmpty(id, nameof(id));
    Name = Guard.Against.NullOrEmpty(name, nameof(name));
    Tags = Guard.Against.NullOrInvalidInput(tags, nameof(tags), items => items.Count() != 0).ToHashSet();
    Price = Money.Create(currency, amount);
    Sku = Sku.Create(sku);
  }
}
