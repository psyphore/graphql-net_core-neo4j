using Ardalis.Result;

using FluentAssertions;

using Mediator;

using NSubstitute;

using ThumbezaTech.Leads.Application.Leads;
using ThumbezaTech.Leads.Domain.LeadAggregate;

namespace ThumbezaTech.Leads.UnitTests.Application.Leads;

public class UpdateLeadHandlerHandle
{
  private readonly UpdateLeadCommandHandler _handler;
  private readonly ILeadService _service;
  private readonly ISender _sender;

  public UpdateLeadHandlerHandle()
  {
    _service = Substitute.For<ILeadService>();
    _sender = Substitute.For<ISender>();
    _handler = new UpdateLeadCommandHandler(_service, _sender);
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
    Func<Task> act = async () => await _handler.Handle(new UpdateLeadCommand(null!), CancellationToken.None);
    await act.Should().ThrowAsync<ArgumentNullException>();
  }

  [Fact]
  public async Task UpdateLeadInstance()
  {
    var lead = GenerateData.GetLead;

    _sender
      .Send(Arg.Any<GetLeadByIdQuery>(), CancellationToken.None)
      .Returns(Result.Success(lead));

    _service
      .UpdateLeadAsync(Arg.Any<Lead>(), CancellationToken.None)
      .Returns(Result.SuccessWithMessage(lead.Id));

    Func<Task> act = async () => await _handler.Handle(new UpdateLeadCommand(lead), CancellationToken.None);
    await act.Should().NotThrowAsync<ArgumentNullException>();
    await _sender.Received().Send(new GetLeadByIdQuery(lead.Id), CancellationToken.None);
    await _service.Received().UpdateLeadAsync(lead, CancellationToken.None);
  }

  [Fact]
  public async Task UpdateLeadMissingInstance()
  {
    var lead = GenerateData.GetLead;

    _sender
      .Send(Arg.Any<GetLeadByIdQuery>(), CancellationToken.None)
      .Returns(Result.NotFound());

    _service
      .UpdateLeadAsync(Arg.Any<Lead>(), CancellationToken.None)
      .Returns(Result.SuccessWithMessage(lead.Id));

    Func<Task> act = async () => await _handler.Handle(new UpdateLeadCommand(lead), CancellationToken.None);
    await act.Should().NotThrowAsync<ArgumentNullException>();
    await _sender.Received().Send(new GetLeadByIdQuery(lead.Id), CancellationToken.None);
    await _service.Received().UpdateLeadAsync(lead, CancellationToken.None);
  }
}
