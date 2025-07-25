using EnviosYa.Application.Common.Abstractions;
using EnviosYa.Domain.Common;
using EnviosYa.Infrastructure.Authentication;
using EnviosYa.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EnviosYa.Infrastructure;

public static class DependencyInjection
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddRepository(configuration);
        //services.AddSingleton<ITokenGenerator, JwtTokenGenerator>();
    }

    private static void AddRepository(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("EnviosYaConnection");

        if (string.IsNullOrEmpty(connectionString))
            throw new InvalidOperationException("Connection string 'EnviosYaConnection' is not configured.");

        if (connectionString.Contains("enviosya_db"))
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(connectionString));
        }

        services.AddScoped<IRepository>(provider => provider.GetRequiredService<ApplicationDbContext>());
        services.AddSingleton<IPasswordHasher, PasswordHasher>();
        services.AddSingleton<ITokenGenerator, JwtTokenGenerator>();
    }
}