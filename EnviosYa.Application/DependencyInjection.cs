using EnviosYa.Application.Common.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace EnviosYa.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.Scan(scan => scan
            .FromAssembliesOf(typeof(ICommandHandler<,>))
            .AddClasses(classes =>
                    classes.AssignableTo(typeof(ICommandHandler<,>)),
                publicOnly: false)
            .AsImplementedInterfaces()
            .WithScopedLifetime()
            .AddClasses(classes =>
                    classes.AssignableTo(typeof(ICommandHandler<>)),
                publicOnly: false)
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        return services;
    }
}