using Microsoft.IdentityModel.JsonWebTokens;

namespace ServiTec.Infrastructure.Security;

/// <summary>
/// Claim names used in issued tokens. Inbound claim mapping is disabled, so these are
/// the exact claim types seen in <c>HttpContext.User</c>.
/// </summary>
public static class AuthClaimTypes
{
    public const string UserId = JwtRegisteredClaimNames.Sub;
    public const string Email = JwtRegisteredClaimNames.Email;
    public const string Name = JwtRegisteredClaimNames.Name;
    public const string Role = "role";
}
