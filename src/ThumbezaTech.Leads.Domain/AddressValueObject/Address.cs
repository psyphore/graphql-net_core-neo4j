namespace ThumbezaTech.Leads.Domain.AddressValueObject;
public sealed record Address(string Line1, string Line2, string? Line3, string Suburb, string Zip, string Country, IEnumerable<string> ContactNumbers);
