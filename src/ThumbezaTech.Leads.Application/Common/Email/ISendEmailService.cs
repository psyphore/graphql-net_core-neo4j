namespace ThumbezaTech.Leads.Application.Common.Email;
public interface ISendEmailService
{
  Task SendEmailAsync(Dictionary<string, object> message, CancellationToken cancellationToken);
}
