namespace ThumbezaTech.Leads.Application.Common.Email;

public sealed record SendEmailEvent(IEnumerable<string> Receipent, string Subject, string Message, IDictionary<string, object> Attachments) : INotification;

internal sealed class SendEmailEventHandler : INotificationHandler<SendEmailEvent>
{
  private readonly ISendEmailService _service;

  public SendEmailEventHandler(ISendEmailService service) => _service = service;

  public async ValueTask Handle(SendEmailEvent notification, CancellationToken cancellationToken)
  {
    Guard.Against.Null(notification, nameof(notification));
    Dictionary<string, object> message = new()
    {
      { nameof(notification.Receipent), string.Join(";", Guard.Against.NullOrEmpty(notification.Receipent)) },
      { nameof(notification.Subject), Guard.Against.NullOrEmpty(notification.Subject) },
      { nameof(notification.Message), Guard.Against.NullOrEmpty(notification.Message)},
      { nameof(notification.Attachments), notification.Attachments }
    };

    await _service.SendEmailAsync(message, cancellationToken);
    //if (!sent.IsSuccess)
    //  throw new 
  }
}
