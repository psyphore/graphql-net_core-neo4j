using Ardalis.GuardClauses;

namespace ThumbezaTech.Leads.Domain.ProductAggregate;

public sealed record Money
{
  public string Currency { get; init; }
  public decimal Amount { get; init; }

  internal Money(string currency, decimal amount)
    => (Currency, Amount) = (Guard.Against.NullOrEmpty(currency), Guard.Against.Negative(amount));

  public static Money Create(string currency, decimal amount)
  {
    var value = new Money(currency, amount);
    return value;
  }
}
