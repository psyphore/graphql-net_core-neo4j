using FluentValidation;

using ThumbezaTech.Leads.Domain.LeadAggregate;

namespace ThumbezaTech.Leads.Application.Leads;
public sealed record CreateLeadCommand(Lead Lead) : ICommand<Result>;

internal sealed class CreateLeadCommandHandler : ICommandHandler<CreateLeadCommand, Result>
{
  private readonly ILeadService _service;

  public CreateLeadCommandHandler(ILeadService service) => _service = service;

  public ValueTask<Result> Handle(CreateLeadCommand command, CancellationToken cancellationToken)
  {
    Guard.Against.Null(command, nameof(command));
    return _service.CreateALeadAsync(Guard.Against.Null(command.Lead, nameof(command.Lead)), cancellationToken);
  }
}

internal sealed class CreateLeadCommandValidator: AbstractValidator<CreateLeadCommand>
{
  public CreateLeadCommandValidator()
  {
    RuleFor(m => m.Lead).NotNull();
    RuleFor(m => m.Lead.FirstName).NotEmpty().NotNull();
    RuleFor(m => m.Lead.LastName).NotEmpty().NotNull();
    RuleFor(m => m.Lead.Id).Empty().Null();
    RuleFor(m => m.Lead.Active).Must(v => false);
    RuleFor(m => m.Lead.Contacts)
      .NotEmpty()
      .NotNull()
      .Must(v => v.All(i => !string.IsNullOrEmpty(i.Number)));
    RuleFor(m => m.Lead.DateOfBirth).InclusiveBetween(DateTimeOffset.MinValue, DateTimeOffset.Now);
  }
}
