using Ardalis.GuardClauses;

namespace ThumbezaTech.Leads.Domain.ProductAggregate;

// Stock Keeping Unit
public sealed record Sku
{
  private const int DefaultSkuLength = 16;
  public string Value { get; init; }

  internal Sku(string value) => Value = value;

  public static Sku Create(string value)
  {
    Guard.Against.NullOrEmpty(value, nameof(value));
    Guard.Against.InvalidInput(value, nameof(value), i => i.Length != DefaultSkuLength);
    return new Sku(value);
  }
}
