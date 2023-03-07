using Ardalis.GuardClauses;

using ThumbezaTech.Leads.SharedKernel.Interfaces;

namespace ThumbezaTech.Leads.Domain.ProductAggregate;

public sealed class Product : BaseEntity, IAggregateRoot
{
    public string Name { get; private set; } = default!;
    public decimal Value { get; private set; } = default!;
    private readonly List<string> _tags = new();
    public IEnumerable<string> Tags => _tags.AsReadOnly();

    public Product(string name, decimal value, List<string> tags)
    {
        Name = Guard.Against.NullOrEmpty(name, nameof(name));
        Value = Guard.Against.NegativeOrZero(value, nameof(value));
        _tags = Guard.Against.NullOrInvalidInput(tags, nameof(tags), items => items.Any());
    }
}
