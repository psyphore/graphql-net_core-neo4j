namespace ThumbezaTech.Leads.Application.Common.Email;
public interface ISendEmailService
{
  ValueTask<Result> SendEmailAsync(Dictionary<string, object> message, CancellationToken cancellationToken);
}
