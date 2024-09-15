namespace ThumbezaTech.Leads.Infrastructure.Email;

internal sealed record SmtpConfiguration
{
  public string Sender { get; set; }
  public string SenderName { get; set; }
  public string SmtpServer { get; set; }
  public int Port { get; set; }
}
