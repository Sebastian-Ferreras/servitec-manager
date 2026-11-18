using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ServiTec.Application.Interfaces.Security;
using ServiTec.Domain.Entities;
using ServiTec.Domain.Enums;

namespace ServiTec.Infrastructure.Persistence;

/// <summary>
/// Development-only bootstrap: applies pending migrations and seeds one user per role.
/// </summary>
public static class DbInitializer
{
    private const string DefaultPasswordKey = "Seed:DefaultPassword";

    private static readonly (string FullName, string Email, UserRole Role)[] SeedUsers =
    [
        ("Administrador ServiTec", "admin@servitec.local", UserRole.Administrator),
        ("Recepción ServiTec", "recepcion@servitec.local", UserRole.Reception),
        ("Técnico ServiTec", "tecnico@servitec.local", UserRole.Technician)
    ];

    public static async Task InitializeAsync(IServiceProvider serviceProvider, CancellationToken cancellationToken = default)
    {
        await using var scope = serviceProvider.CreateAsyncScope();
        var services = scope.ServiceProvider;

        var context = services.GetRequiredService<AppDbContext>();
        var configuration = services.GetRequiredService<IConfiguration>();
        var passwordHasher = services.GetRequiredService<IPasswordHasher>();
        var logger = services.GetRequiredService<ILoggerFactory>().CreateLogger(typeof(DbInitializer));

        await context.Database.MigrateAsync(cancellationToken);
        await SeedUsersAsync(context, configuration, passwordHasher, logger, cancellationToken);
    }

    private static async Task SeedUsersAsync(
        AppDbContext context,
        IConfiguration configuration,
        IPasswordHasher passwordHasher,
        ILogger logger,
        CancellationToken cancellationToken)
    {
        var seedEmails = SeedUsers.Select(u => u.Email).ToArray();
        var existingEmails = await context.Users
            .Where(u => seedEmails.Contains(u.Email))
            .Select(u => u.Email)
            .ToListAsync(cancellationToken);

        var missing = SeedUsers.Where(u => !existingEmails.Contains(u.Email)).ToList();
        if (missing.Count == 0)
        {
            return;
        }

        var defaultPassword = configuration[DefaultPasswordKey];
        if (string.IsNullOrWhiteSpace(defaultPassword))
        {
            throw new InvalidOperationException(
                $"'{DefaultPasswordKey}' is not configured. Set the environment variable 'Seed__DefaultPassword' (see .env.example) to seed the initial users.");
        }

        foreach (var (fullName, email, role) in missing)
        {
            context.Users.Add(new User
            {
                FullName = fullName,
                Email = email,
                PasswordHash = passwordHasher.Hash(defaultPassword),
                Role = role,
                IsActive = true
            });
        }

        await context.SaveChangesAsync(cancellationToken);
        logger.LogInformation(
            "Seeded {Count} initial user(s): {Emails}",
            missing.Count,
            string.Join(", ", missing.Select(u => u.Email)));
    }
}
