using FluentValidation;

using Microsoft.Extensions.DependencyInjection;

using ThumbezaTech.Leads.Application.Common.Behaviours;

namespace ThumbezaTech.Leads.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = typeof(DependencyInjection).Assembly;

        services.AddMediator(options => options.ServiceLifetime = ServiceLifetime.Scoped);
        services.AddValidatorsFromAssembly(assembly);

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));

        return services;
    }
}