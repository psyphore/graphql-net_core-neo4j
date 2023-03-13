using Ardalis.Result;

using FluentEmail.Core;

using ThumbezaTech.Leads.Application.Common.Email;

namespace ThumbezaTech.Leads.Infrastructure.Email.Services;
internal sealed class SendEmailService : ISendEmailService
{
  private readonly IFluentEmailFactory _factory;
  public SendEmailService(IFluentEmailFactory factory) => _factory = factory;

  public async ValueTask<Result> SendEmailAsync(Dictionary<string, object> message, CancellationToken cancellationToken)
  {
    var email = _factory
            .Create()
            .To(Translate<string>("Receipent", message))
            .Subject(Translate<string>("Subject", message))
            .Body(Translate<string>("Message", message), true);

    var response = await email.SendAsync(cancellationToken);
    return response.Successful
      ? Result.SuccessWithMessage(response.MessageId)
      : Result.Error(string.Join("; ", response.ErrorMessages));
  }

  private static T Translate<T>(string key, IDictionary<string, object> payload)
    => (T)payload[key];
}
