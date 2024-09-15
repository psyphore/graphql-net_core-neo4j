using System.Text;

using Microsoft.Extensions.Logging;

using Newtonsoft.Json;

using ThumbezaTech.Leads.Application.Common.Email;

namespace ThumbezaTech.Leads.Application.Common.Behaviours;

internal sealed class UnhandledExceptionBehaviour<TRequest, TResponse> : MessageExceptionHandler<TRequest, TResponse>
  where TRequest : notnull, IMessage
{
  private readonly ILogger<TRequest> _logger;
  private readonly IMediator _mediator;

  public UnhandledExceptionBehaviour(ILogger<TRequest> logger)
  {
    _logger = logger;
  }

  protected override async ValueTask<ExceptionHandlingResult<TResponse>> Handle(TRequest message, Exception exception, CancellationToken cancellationToken)
  {
    var requestName = typeof(TRequest).Name;
    var appName = "Masngcwabisane";
    _logger.LogError(exception, "ThumbezaTech.Leads Request: Unhandled Exception for Request {Name} {Request}", requestName, message);

    var body = new StringBuilder("Hello");
    body.AppendLine()
    .AppendLine($"{appName} Request: Unhandled Exception for Request {requestName}.")
    .AppendLine()
    .AppendLine(JsonConvert.SerializeObject(message, Formatting.Indented))
    .AppendLine()
    .AppendLine("Kind Regards, Masngcwabisane Team")
    ;

    await _mediator.Publish(new SendEmailEvent(["admin@masngcwabisane.thumbezatech.co.za"], "Request Error", body.ToString(), default!), cancellationToken).ConfigureAwait(false);

    return Handled(default!);
  }
}
