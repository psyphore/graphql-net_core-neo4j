using Ardalis.Result;

using FluentAssertions;

using NSubstitute;

using ThumbezaTech.Leads.Application.Leads;
using ThumbezaTech.Leads.Domain.LeadAggregate;

namespace ThumbezaTech.Leads.UnitTests.Application.Leads;
public class CreateLeadHandlerHandle
{
  private readonly CreateLeadCommandHandler _handler;
  private readonly ILeadService _service;

  public CreateLeadHandlerHandle()
  {
    _service = Substitute.For<ILeadService>();
    _handler = new CreateLeadCommandHandler(_service);
  }

  [Fact]
  public async Task ThrowsExceptionGivenNullCommandArg()
  {
    Func<Task> act = async () => await _handler.Handle(null!, CancellationToken.None);
    await act.Should().ThrowAsync<ArgumentNullException>();
  }

  [Fact]
  public async Task ThrowsExceptionGivenNullLeadArg()
  {
    Func<Task> act = async () => await _handler.Handle(new CreateLeadCommand(null!), CancellationToken.None);
    await act.Should().ThrowAsync<ArgumentNullException>();
  }

  [Fact]
  public async Task CreateLeadInstance()
  {
    var lead = GenerateData.GetLead;

    _service
      .CreateALeadAsync(Arg.Any<Lead>(), CancellationToken.None)
      .Returns(Result.SuccessWithMessage(lead.Id));

    Func<Task> act = async () => await _handler.Handle(new CreateLeadCommand(lead), CancellationToken.None);
    await act.Should().NotThrowAsync<ArgumentNullException>();

    await _service.Received().CreateALeadAsync(Arg.Any<Lead>(), CancellationToken.None);
  }
}
