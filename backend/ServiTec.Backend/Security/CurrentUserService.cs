using ServiTec.Application.Interfaces;
using ServiTec.Domain.Enums;
using ServiTec.Infrastructure.Security;

namespace ServiTec.Backend.Services;

public sealed class CurrentUserService(IHttpContextAccessor httpContextAccessor) : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    private System.Security.Claims.ClaimsPrincipal? Principal => _httpContextAccessor.HttpContext?.User;

    public bool IsAuthenticated => Principal?.Identity?.IsAuthenticated ?? false;

    public Guid? UserId =>
        Guid.TryParse(Principal?.FindFirst(AuthClaimTypes.UserId)?.Value, out var id) ? id : null;

    public string? Email => Principal?.FindFirst(AuthClaimTypes.Email)?.Value;

    public UserRole? Role =>
        Enum.TryParse<UserRole>(Principal?.FindFirst(AuthClaimTypes.Role)?.Value, ignoreCase: false, out var role)
            ? role
            : null;
}
