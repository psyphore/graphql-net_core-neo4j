using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using ThumbezaTech.Leads.Application.Common.Email;

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
            .AddSmtpSender(conf.SmtpServer, conf.Port);

    services.AddHttpContextAccessor();
    services.AddRazorPages();

    services.AddTransient<ISendEmailService, SendEmailService>();

    return services;
  }
}
