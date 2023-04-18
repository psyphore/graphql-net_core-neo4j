using Ardalis.Result;

using FluentAssertions;

using Mediator;

using Moq;

using ThumbezaTech.Leads.Application.Leads;
using ThumbezaTech.Leads.Domain.LeadAggregate;

namespace ThumbezaTech.Leads.UnitTests.Application.Leads;

public class UpdateLeadHandlerHandle
{
  private readonly UpdateLeadCommandHandler _handler;
  private readonly Mock<ILeadService> _service;
  private readonly Mock<ISender> _sender;

  public UpdateLeadHandlerHandle()
  {
    _service = new Mock<ILeadService>();
    _sender = new Mock<ISender>();
    _handler = new UpdateLeadCommandHandler(_service.Object, _sender.Object);
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
      .Setup(s => s.Send(It.IsAny<GetLeadByIdQuery>(), CancellationToken.None))
      .ReturnsAsync(Result.Success(lead));

    _service
      .Setup(s => s.UpdateLeadAsync(It.IsAny<Lead>(), CancellationToken.None))
      .ReturnsAsync(Result.SuccessWithMessage(lead.Id));

    Func<Task> act = async () => await _handler.Handle(new UpdateLeadCommand(lead), CancellationToken.None);
    await act.Should().NotThrowAsync<ArgumentNullException>();
    _sender.Verify(s => s.Send(new GetLeadByIdQuery(lead.Id), CancellationToken.None), Times.Once);
    _service.Verify(s => s.UpdateLeadAsync(lead, CancellationToken.None), Times.Once);
  }

  [Fact]
  public async Task UpdateLeadMissingInstance()
  {
    var lead = GenerateData.GetLead;

    _sender
      .Setup(s => s.Send(It.IsAny<GetLeadByIdQuery>(), CancellationToken.None))
      .ReturnsAsync(Result.NotFound());

    _service
      .Setup(s => s.UpdateLeadAsync(It.IsAny<Lead>(), CancellationToken.None))
      .ReturnsAsync(Result.SuccessWithMessage(lead.Id));

    Func<Task> act = async () => await _handler.Handle(new UpdateLeadCommand(lead), CancellationToken.None);
    await act.Should().NotThrowAsync<ArgumentNullException>();
    _sender.Verify(s => s.Send(new GetLeadByIdQuery(lead.Id), CancellationToken.None), Times.Once);
    _service.Verify(s => s.UpdateLeadAsync(lead, CancellationToken.None), Times.Never);
  }
}
