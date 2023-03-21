using Bogus;

using ThumbezaTech.Leads.Domain.AddressValueObject;
using ThumbezaTech.Leads.Domain.ContactValueObject;
using ThumbezaTech.Leads.Domain.LeadAggregate;

namespace ThumbezaTech.Leads.UnitTests.Application.Leads;
public static class GenerateData
{
  private static readonly Faker _faker = new();
  private const int Size = 100;

  public static IEnumerable<Lead> GetLeads(int size = Size)
    => Enumerable.Range(0, size)
    .Select(i => new Lead(
      _faker.Person.FirstName,
      _faker.Person.LastName,
      _faker.Person.DateOfBirth,
      false,
      new[]
      {
        new Contact(_faker.Person.Phone, _faker.Person.Email)
      },
      new[]
      {
        new Address(_faker.Address.BuildingNumber(),
                    _faker.Address.StreetAddress(),
                    default!,
                    _faker.Address.StreetSuffix(),
                    _faker.Address.ZipCode(),
                    _faker.Address.Country())
      })
    { Id = _faker.Random.Uuid().ToString() })
    ;

  public static Lead GetLead => GetLeads().ToArray()[Random.Shared.Next(Size)];
}
