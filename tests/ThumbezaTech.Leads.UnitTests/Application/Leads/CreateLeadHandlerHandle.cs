using Ardalis.Result;

using FluentAssertions;

using Moq;

using ThumbezaTech.Leads.Application.Leads;
using ThumbezaTech.Leads.Domain.LeadAggregate;

namespace ThumbezaTech.Leads.UnitTests.Application.Leads;
public class CreateLeadHandlerHandle
{
  private readonly CreateLeadCommandHandler _handler;
  private readonly Mock<ILeadService> _service;

  public CreateLeadHandlerHandle()
  {
    _service = new Mock<ILeadService>();
    _handler = new CreateLeadCommandHandler(_service.Object);
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
      .Setup(s => s.CreateALeadAsync(It.IsAny<Lead>(), CancellationToken.None))
      .ReturnsAsync(Result.SuccessWithMessage(lead.Id));

    Func<Task> act = async () => await _handler.Handle(new CreateLeadCommand(lead), CancellationToken.None);
    await act.Should().NotThrowAsync<ArgumentNullException>();

    _service.Verify(s => s.CreateALeadAsync(It.IsAny<Lead>(), CancellationToken.None), Times.Once);
  }
}
