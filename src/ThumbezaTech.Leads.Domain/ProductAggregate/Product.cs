using Ardalis.GuardClauses;

using ThumbezaTech.Leads.SharedKernel.Interfaces;

namespace ThumbezaTech.Leads.Domain.ProductAggregate;

public sealed class Product : BaseEntity, IAggregateRoot
{
  private readonly HashSet<string> _tags = new();
  public string Name { get; private set; } = default!;
  public Money Price { get; private set; } = default!;


  public IEnumerable<string> Tags => _tags.ToList().AsReadOnly();

  public Sku Sku { get; private set; } = default!;

  public Product(string name, Money value, string sku, List<string> tags)
  {
    Name = Guard.Against.NullOrEmpty(name, nameof(name));
    Price = Guard.Against.Null(value, nameof(value));
    _tags = Guard.Against.NullOrInvalidInput(tags, nameof(tags), items => items.Count != 0).ToHashSet();
    Sku = Sku.Create(sku);
  }
}
