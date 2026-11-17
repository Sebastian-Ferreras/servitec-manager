using ServiTec.Domain.Enums;

namespace ServiTec.Application.Interfaces;

/// <summary>Information about the authenticated caller of the current request.</summary>
public interface ICurrentUserService
{
    Guid? UserId { get; }
    string? Email { get; }
    UserRole? Role { get; }
    bool IsAuthenticated { get; }
}
