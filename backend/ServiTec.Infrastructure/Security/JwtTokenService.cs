using System.Security.Claims;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using ServiTec.Application.DTOs.Common.Auth;
using ServiTec.Application.Interfaces;
using ServiTec.Domain.Entities;

namespace ServiTec.Infrastructure.Security;

public sealed class JwtTokenService(IOptions<JwtOptions> options, TimeProvider timeProvider) : ITokenService
{
    private readonly JwtOptions _options = options.Value;
    private readonly TimeProvider _timeProvider = timeProvider;
    private readonly JsonWebTokenHandler _tokenHandler = new();

    public TokenResult GenerateToken(User user)
    {
        var now = _timeProvider.GetUtcNow().UtcDateTime;
        var expiresAt = now.AddMinutes(_options.ExpiresMinutes);

        var descriptor = new SecurityTokenDescriptor
        {
            Issuer = _options.Issuer,
            Audience = _options.Audience,
            IssuedAt = now,
            NotBefore = now,
            Expires = expiresAt,
            Subject = new ClaimsIdentity(
            [
                new Claim(AuthClaimTypes.UserId, user.Id.ToString()),
                new Claim(AuthClaimTypes.Email, user.Email),
                new Claim(AuthClaimTypes.Name, user.FullName),
                new Claim(AuthClaimTypes.Role, user.Role.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            ]),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(_options.GetKeyBytes()),
                SecurityAlgorithms.HmacSha256)
        };

        return new TokenResult(_tokenHandler.CreateToken(descriptor), expiresAt);
    }
}
