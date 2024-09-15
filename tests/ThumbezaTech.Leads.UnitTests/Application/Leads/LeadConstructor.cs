using FluentAssertions;

using ThumbezaTech.Leads.Domain.LeadAggregate;
using ThumbezaTech.Leads.Domain.LeadAggregate.Events;

namespace ThumbezaTech.Leads.UnitTests.Application.Leads;

public class LeadConstructor
{
  private Lead? _lead;

  [Fact]
  public Task ThrowsExceptionGivenNullArgs()
  {
    Action action = () => _lead = new Lead(default!, default!, default!, default!, default!, default!);
    action.Should().Throw<ArgumentNullException>();
    return Task.CompletedTask;
  }

  [Fact]
  public Task CreateLeadGivenWithArgs()
  {
    Action action = () => _lead = GenerateData.GetLead;
    action.Should().NotThrow<ArgumentNullException>();

    _lead.Active.Should().BeFalse();
    _lead.Events.Should().HaveCount(1);
    _lead.Events.Should().Contain(e => e is LeadCreatedEvent);

    return Task.CompletedTask;
  }

  [Fact]
  public Task UpdateLeadGivenWithArgs()
  {
    _lead = GenerateData.GetLead;

    _lead.Addresses.Should().HaveCount(1);
    _lead.Contacts.Should().HaveCount(1);

    _lead.Update(GenerateData.GetLeads().Last());

    _lead.Events.Should().HaveCount(2);

    _lead.Events.Should().Contain(e => e is LeadCreatedEvent);
    _lead.Events.Should().Contain(e => e is LeadUpdatedEvent);

    return Task.CompletedTask;
  }

  [Fact]
  public Task ActivateLead()
  {
    _lead = GenerateData.GetLead;
    _lead.Activate();

    _lead.Active.Should().BeTrue();
    _lead.Events.Should().HaveCount(2);

    _lead.Events.Should().Contain(e => e is LeadCreatedEvent);
    _lead.Events.Should().Contain(e => e is LeadActivedEvent);

    return Task.CompletedTask;
  }

  [Fact]
  public Task AbandonLead()
  {
    _lead = GenerateData.GetLead;
    _lead.Activate();
    _lead.Abandon();

    _lead.Active.Should().BeFalse();
    _lead.Events.Should().HaveCount(3);

    _lead.Events.Should().Contain(e => e is LeadCreatedEvent);
    _lead.Events.Should().Contain(e => e is LeadActivedEvent);
    _lead.Events.Should().Contain(e => e is LeadAbandonedEvent);

    return Task.CompletedTask;
  }

  [Fact]
  public Task DeleteLead()
  {
    _lead = GenerateData.GetLead;
    _lead.Delete();

    _lead.Events.Should().HaveCount(2);

    _lead.Events.Should().Contain(e => e is LeadCreatedEvent);
    _lead.Events.Should().Contain(e => e is LeadDeletedEvent);

    return Task.CompletedTask;
  }
}
