using Ardalis.GuardClauses;

using Newtonsoft.Json;

using ThumbezaTech.Leads.Domain.AddressValueObject;
using ThumbezaTech.Leads.Domain.ContactValueObject;
using ThumbezaTech.Leads.Domain.LeadAggregate.Events;
using ThumbezaTech.Leads.SharedKernel.Interfaces;

namespace ThumbezaTech.Leads.Domain.LeadAggregate;

public sealed class Lead : BaseEntity, IAggregateRoot
{
  public string FirstName { get; private set; }
  public string LastName { get; private set; }
  public DateTimeOffset DateOfBirth { get; private set; }
  public bool Active { get; private set; }
  public IEnumerable<Contact>? Contacts { get; private set; }
  public IEnumerable<Address>? Addresses { get; private set; }

  public Lead(string firstName, string lastName, DateTimeOffset dob, bool active, IEnumerable<Contact>? contacts, IEnumerable<Address>? addresses)
  {
    FirstName = Guard.Against.NullOrEmpty(firstName, nameof(firstName));
    LastName = Guard.Against.NullOrEmpty(lastName, nameof(lastName));
    DateOfBirth = Guard.Against.OutOfSQLDateRange(dob.DateTime, nameof(dob));
    Active = active;

    Contacts = contacts;
    Addresses = addresses;

    Events.Add(new LeadCreatedEvent(this));
  }

  [JsonConstructor]
  internal Lead() { }

  public void Update(Lead lead)
  {
    Guard.Against.Null(lead, nameof(lead));

    FirstName = Guard.Against.NullOrEmpty(lead.FirstName, nameof(FirstName));
    LastName = Guard.Against.NullOrEmpty(lead.LastName, nameof(LastName));
    DateOfBirth = Guard.Against.OutOfSQLDateRange(lead.DateOfBirth.DateTime, nameof(DateOfBirth));
    Active = lead.Active;

    Contacts = lead.Contacts;
    Addresses = lead.Addresses;

    Events.Add(new LeadUpdatedEvent(lead));
  }

  public void Delete() => Events.Add(new LeadDeletedEvent(this));

  public void Activate()
  {
    Active = true;
    Events.Add(new LeadActivedEvent(Id));
  }

  public void Abandon()
  {
    Active = false;
    Events.Add(new LeadAbandonedEvent(Id));
  }
}
