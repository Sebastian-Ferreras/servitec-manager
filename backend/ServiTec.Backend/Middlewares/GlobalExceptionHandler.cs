using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ServiTec.Domain.Exceptions;

namespace ServiTec.Backend.Middlewares;

/// <summary>
/// Translates domain exceptions into ProblemDetails responses.
/// Unknown exceptions become a generic 500 without leaking internal details.
/// </summary>
public sealed class GlobalExceptionHandler(
    IProblemDetailsService problemDetailsService,
    ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
    private readonly IProblemDetailsService _problemDetailsService = problemDetailsService;
    private readonly ILogger<GlobalExceptionHandler> _logger = logger;

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        var problemDetails = exception switch
        {
            ValidationException ex => new ValidationProblemDetails(ex.Errors.ToDictionary(e => e.Key, e => e.Value))
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Validation error",
                Detail = ex.Message
            },
            NotFoundException ex => Create(StatusCodes.Status404NotFound, "Resource not found", ex.Message),
            ConflictException ex => Create(StatusCodes.Status409Conflict, "Conflict", ex.Message),
            UnauthorizedException ex => Create(StatusCodes.Status401Unauthorized, "Unauthorized", ex.Message),
            ForbiddenException ex => Create(StatusCodes.Status403Forbidden, "Forbidden", ex.Message),
            BusinessRuleException ex => Create(StatusCodes.Status422UnprocessableEntity, "Business rule violation", ex.Message),
            _ => Create(StatusCodes.Status500InternalServerError, "Internal server error", "An unexpected error occurred.")
        };

        if (problemDetails.Status == StatusCodes.Status500InternalServerError)
        {
            _logger.LogError(exception, "Unhandled exception processing {Method} {Path}", httpContext.Request.Method, httpContext.Request.Path);
        }

        httpContext.Response.StatusCode = problemDetails.Status!.Value;

        return await _problemDetailsService.TryWriteAsync(new ProblemDetailsContext
        {
            HttpContext = httpContext,
            ProblemDetails = problemDetails,
            Exception = exception
        });
    }

    private static ProblemDetails Create(int status, string title, string detail) => new()
    {
        Status = status,
        Title = title,
        Detail = detail
    };
}
