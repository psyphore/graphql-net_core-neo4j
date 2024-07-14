using Ardalis.Result;

using FluentEmail.Core;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using ThumbezaTech.Leads.Application.Common.Email;

namespace ThumbezaTech.Leads.Infrastructure.Email;
internal sealed class SendEmailService : ISendEmailService
{
  private readonly ILogger<SendEmailService> _logger;
  private readonly IServiceScopeFactory _provider;
  private readonly SmtpConfiguration _config;

  public SendEmailService(IOptions<SmtpConfiguration> config,
                          IServiceScopeFactory provider,
                          ILogger<SendEmailService> logger)
  {
    _config = config.Value;
    _provider = provider;
    _logger = logger;
  }

  public Task SendEmailAsync(Dictionary<string, object> message, CancellationToken cancellationToken)
  {
    using var scope = _provider.CreateScope();

    var email = scope.ServiceProvider
      .GetRequiredService<IFluentEmail>()
            .To(Translate<string>("Receipent", message))
            .Subject(Translate<string>("Subject", message))
            .Body(Translate<string>("Message", message), true);

    return Send(email, cancellationToken);
  }

  private static T Translate<T>(string key, IDictionary<string, object> payload)
    => (T)payload[key];

  private async Task Send(IFluentEmail email, CancellationToken cancellationToken)
  {
    var response = await email.SendAsync(cancellationToken).ConfigureAwait(true);
    if (response.Successful)
      _logger.LogInformation("Message Sent: {@Response}", response);
    else _logger.LogWarning("Message Failed: {@Error}", response);

  }
}
