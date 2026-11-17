using System.Reflection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using ServiTec.Application.Interfaces;
using ServiTec.Infrastructure;

namespace ServiTec.Backend.Extensions;

public static class ServiceRegistrationExtensions
{
    private static readonly string[] ConventionSuffixes = ["Service", "Repository"];

    /// <summary>
    /// Registers as Scoped every concrete class in the Application and Infrastructure assemblies whose
    /// name ends with "Service" or "Repository" against its matching interface I&lt;ClassName&gt;.
    /// Classes without such an interface are ignored. Existing registrations are never overridden.
    /// </summary>
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        Assembly[] assemblies =
        [
            typeof(IAuthService).Assembly,          // ServiTec.Application
            typeof(DependencyInjection).Assembly    // ServiTec.Infrastructure
        ];

        var candidates = assemblies
            .SelectMany(a => a.GetTypes())
            .Where(t => t is { IsClass: true, IsAbstract: false, IsGenericTypeDefinition: false }
                        && ConventionSuffixes.Any(suffix => t.Name.EndsWith(suffix, StringComparison.Ordinal)));

        foreach (var implementation in candidates)
        {
            var serviceInterface = implementation.GetInterfaces()
                .FirstOrDefault(i => i.Name == $"I{implementation.Name}");

            if (serviceInterface is not null)
            {
                services.TryAddScoped(serviceInterface, implementation);
            }
        }

        return services;
    }
}
