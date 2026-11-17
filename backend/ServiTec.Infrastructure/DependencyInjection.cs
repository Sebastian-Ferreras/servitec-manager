using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServiTec.Application.Interfaces;
using ServiTec.Infrastructure.Persistence;
using ServiTec.Infrastructure.Security;

namespace ServiTec.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("Connection string 'DefaultConnection' is not configured.");

        services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionString));

        var jwtSection = configuration.GetSection(JwtOptions.SectionName);
        (jwtSection.Get<JwtOptions>() ?? new JwtOptions()).EnsureValid();
        services.Configure<JwtOptions>(jwtSection);

        services.AddSingleton(TimeProvider.System);

        // These class names don't match their interface as I<ClassName>,
        // so the naming convention skips them and they are registered here.
        services.AddScoped<ITokenService, JwtTokenService>();
        services.AddSingleton<IPasswordHasher, BcryptPasswordHasher>();

        return services;
    }
}
