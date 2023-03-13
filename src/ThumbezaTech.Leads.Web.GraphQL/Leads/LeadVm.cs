using ThumbezaTech.Leads.Domain.LeadAggregate;

namespace ThumbezaTech.Leads.Web.GraphQL.Leads;

public sealed record LeadVm
{
  public string Id { get; private set; }
  public string FirstName { get; private set; }
  public string LastName { get; private set; }
  public DateTimeOffset DateOfBirth { get; private set; }
  public string MobileNumber { get; private set; }
  public string EmailAddress { get; private set; }

  public static explicit operator LeadVm(Lead lead) => new()
  {
    Id = lead.Id,
    FirstName = lead.FirstName,
    LastName = lead.LastName,
    EmailAddress = lead.EmailAddress,
    MobileNumber = lead.MobileNumber,
    DateOfBirth = lead.DateOfBirth
  };

  public static implicit operator Lead(LeadVm lead) => new(lead.FirstName, lead.LastName, lead.DateOfBirth, lead.MobileNumber, lead.EmailAddress)
  {
    Id = lead.Id
  };
}
