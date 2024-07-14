
using System.Diagnostics.CodeAnalysis;

using FluentValidation;

namespace ThumbezaTech.Leads.Application.Common.Behaviours;

internal sealed class ValidationBehaviour<TRequest, TResponse> : MessagePreProcessor<TRequest, TResponse>
where TRequest : IValidate
{
  private readonly IEnumerable<IValidator<TRequest>> _validators;

  public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators) => _validators = validators;

  protected override async ValueTask Handle(TRequest message, CancellationToken cancellationToken)
  {
    if (_validators.Any())
    {
      var context = new ValidationContext<TRequest>(message);

      var validationResults = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));
      var failures = validationResults.SelectMany(r => r.Errors).Where(f => f != null).ToList();

      if (failures.Count != 0)
        throw new ValidationException(failures);
    }
  }
}


public interface IValidate: IMessage
{
  bool IsValid([NotNullWhen(false)] out ValidationError? error);
}
