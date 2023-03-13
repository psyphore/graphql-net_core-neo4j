using FluentEmail.MailKitSmtp;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using ThumbezaTech.Leads.Application.Common.Email;
using ThumbezaTech.Leads.Infrastructure.Email.Services;

namespace ThumbezaTech.Leads.Infrastructure.Email;
public static class DependencyInjection
{
  public static IServiceCollection AddEmailInfrastructure(this IServiceCollection services, IConfigurationSection section)
  {
    SmtpConfiguration conf = new();
    section.Bind(conf);

    services.AddSingleton(sp => conf);

    services
            .AddFluentEmail(conf.Sender, conf.SenderName)
            .AddRazorRenderer()
            .AddMailKitSender(new SmtpClientOptions
            {
              Server = conf.SmtpServer,
              Port = conf.Port
            });

    services.AddHttpContextAccessor();
    services.AddRazorPages();

    services.AddTransient<ISendEmailService, SendEmailService>();

    return services;
  }
}

internal sealed record SmtpConfiguration
{
  public string Sender { get; set; }
  public string SenderName { get; set; }
  public string SmtpServer { get; set; }
  public int Port { get; set; }
}
