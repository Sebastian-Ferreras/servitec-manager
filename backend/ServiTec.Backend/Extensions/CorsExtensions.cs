namespace ServiTec.Backend.Extensions;

public static class CorsExtensions
{
    public const string FrontendPolicy = "Frontend";

    /// <summary>Allows the frontend origin from "Cors:AllowedOrigin" (env var Cors__AllowedOrigin).</summary>
    public static IServiceCollection AddFrontendCors(this IServiceCollection services, IConfiguration configuration)
    {
        var allowedOrigin = configuration["Cors:AllowedOrigin"];
        if (string.IsNullOrWhiteSpace(allowedOrigin))
        {
            throw new InvalidOperationException("'Cors:AllowedOrigin' is not configured (env var Cors__AllowedOrigin).");
        }

        services.AddCors(options => options.AddPolicy(FrontendPolicy, policy => policy
            .WithOrigins(allowedOrigin.TrimEnd('/'))
            .AllowAnyHeader()
            .AllowAnyMethod()));

        return services;
    }
}
