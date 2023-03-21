using Ardalis.Result;

using Bogus;

using FluentAssertions;

using Moq;

using ThumbezaTech.Leads.Application.Leads;
using ThumbezaTech.Leads.Domain.AddressValueObject;
using ThumbezaTech.Leads.Domain.ContactValueObject;
using ThumbezaTech.Leads.Domain.LeadAggregate;

namespace ThumbezaTech.Leads.UnitTests.Application.Leads;
public class CreateLeadHandlerHandle
{
  private readonly CreateLeadCommandHandler _handler;
  private readonly Mock<ILeadService> _service;
  private readonly Faker _faker;

  public CreateLeadHandlerHandle()
  {
    _service = new Mock<ILeadService>();
    _handler = new CreateLeadCommandHandler(_service.Object);

    Faker.GlobalUniqueIndex = 1;
    _faker = new Faker();
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
    var lead = new Lead(
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
      });

    _service
      .Setup(s => s.CreateALeadAsync(It.IsAny<Lead>(), CancellationToken.None))
      .ReturnsAsync(Result.SuccessWithMessage(_faker.Random.Uuid().ToString()));

    Func<Task> act = async () => await _handler.Handle(new CreateLeadCommand(lead), CancellationToken.None);
    await act.Should().NotThrowAsync<ArgumentNullException>();

    _service.Verify(s => s.CreateALeadAsync(It.IsAny<Lead>(), CancellationToken.None), Times.Once);
  }
}
