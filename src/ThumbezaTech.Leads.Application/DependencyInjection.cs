using FluentValidation;

using Microsoft.Extensions.DependencyInjection;

using ThumbezaTech.Leads.Application.Common.Behaviours;
using ThumbezaTech.TaxiManager.Core.Common.Behaviors;

namespace ThumbezaTech.Leads.Application;

public static class DependencyInjection
{
  public static IServiceCollection AddApplication(this IServiceCollection services)
  {
    var assembly = typeof(DependencyInjection).Assembly;

    services.AddMediator(options => options.ServiceLifetime = ServiceLifetime.Transient);
    services.AddValidatorsFromAssembly(assembly);

    services.AddSingleton(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
    services.AddSingleton(typeof(IPipelineBehavior<,>), typeof(PerformanceBehavior<,>));
    services.AddSingleton(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));

    return services;
  }
}
