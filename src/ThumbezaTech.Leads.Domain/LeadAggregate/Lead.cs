﻿using Ardalis.GuardClauses;

using Newtonsoft.Json;

using ThumbezaTech.Leads.Domain.AddressValueObject;
using ThumbezaTech.Leads.Domain.LeadAggregate.Events;
using ThumbezaTech.Leads.SharedKernel.Interfaces;

namespace ThumbezaTech.Leads.Domain.LeadAggregate;

public sealed class Lead : BaseEntity, IAggregateRoot
{
  public string FirstName { get; private set; }
  public string LastName { get; private set; }
  public DateTimeOffset DateOfBirth { get; private set; }
  public string MobileNumber { get; private set; }
  public string EmailAddress { get; private set; }
  public Address Address { get; private set; }

  public Lead(string firstName, string lastName, DateTimeOffset dob, string mobileNumber, string email, Address address)
  {
    FirstName = Guard.Against.NullOrEmpty(firstName, nameof(firstName));
    LastName = Guard.Against.NullOrEmpty(lastName, nameof(lastName));
    EmailAddress = Guard.Against.NullOrEmpty(email, nameof(email));
    DateOfBirth = Guard.Against.OutOfSQLDateRange(dob.DateTime, nameof(dob));
    MobileNumber = Guard.Against.NullOrEmpty(mobileNumber, nameof(mobileNumber));
    Address = Guard.Against.Null(address, nameof(address));

    Events.Add(new LeadCreatedEvent(this));
  }

  [JsonConstructor]
  internal Lead(string id, string firstName, string lastName, string dateOfBirth, string emailAddress
    // IEnumerable<string>? numbers
    )
  {
    Id = Guard.Against.NullOrEmpty(id, nameof(id));
    FirstName = Guard.Against.NullOrEmpty(firstName, nameof(firstName));
    LastName = Guard.Against.NullOrEmpty(lastName, nameof(lastName));
    EmailAddress = Guard.Against.NullOrEmpty(emailAddress, nameof(emailAddress));
    DateOfBirth = Guard.Against.OutOfSQLDateRange(DateTime.Parse(dateOfBirth), nameof(dateOfBirth));
  }

  public void Update(Lead lead)
  {
    Guard.Against.Null(lead, nameof(lead));

    FirstName = Guard.Against.NullOrEmpty(lead.FirstName, nameof(FirstName));
    LastName = Guard.Against.NullOrEmpty(lead.LastName, nameof(LastName));
    MobileNumber = Guard.Against.NullOrEmpty(lead.MobileNumber, nameof(MobileNumber));
    DateOfBirth = Guard.Against.OutOfSQLDateRange(lead.DateOfBirth.DateTime, nameof(DateOfBirth));
    EmailAddress = Guard.Against.NullOrEmpty(lead.EmailAddress, nameof(EmailAddress));
    Address = Guard.Against.Null(lead.Address, nameof(lead.Address));

    Events.Add(new LeadUpdatedEvent(lead));
  }

  public void Delete() => Events.Add(new LeadDeletedEvent(this));

  public void Activate() => Events.Add(new LeadActivedEvent(Id));

  public void Abandon() => Events.Add(new LeadAbandonedEvent(Id));
}
