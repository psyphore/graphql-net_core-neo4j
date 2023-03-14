using ThumbezaTech.Leads.Domain.AddressValueObject;
using ThumbezaTech.Leads.Domain.ContactValueObject;
using ThumbezaTech.Leads.Domain.LeadAggregate;

namespace ThumbezaTech.Leads.Web.GraphQL.Leads;

public sealed record LeadVm
{
  public string Id { get; private set; }
  public string FirstName { get; private set; }
  public string LastName { get; private set; }
  public DateTimeOffset DateOfBirth { get; private set; }
  public bool Active { get; private set; }
  public IEnumerable<Contact>? Contacts { get; private set; }
  public IEnumerable<Address>? Addresses { get; private set; }

  public static explicit operator LeadVm(Lead lead)
    => new()
    {
      Id = lead.Id,
      FirstName = lead.FirstName,
      LastName = lead.LastName,
      DateOfBirth = lead.DateOfBirth,

      Addresses = lead.Addresses,
      Contacts = lead.Contacts,
      Active = lead.Active
    };

  public static implicit operator Lead(LeadVm lead)
    => new(lead.FirstName, lead.LastName, lead.DateOfBirth, lead.Active, lead.Contacts, lead.Addresses)
    {
      Id = lead.Id
    };
}
