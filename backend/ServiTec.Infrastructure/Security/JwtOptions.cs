using System.Text;

namespace ServiTec.Infrastructure.Security;

/// <summary>JWT settings bound from the "Jwt" configuration section (Jwt__Key, Jwt__Issuer, ...).</summary>
public sealed class JwtOptions
{
    public const string SectionName = "Jwt";
    private const int MinKeyBytes = 32;

    public string Key { get; set; } = string.Empty;
    public string Issuer { get; set; } = string.Empty;
    public string Audience { get; set; } = string.Empty;
    public int ExpiresMinutes { get; set; } = 60;

    public byte[] GetKeyBytes() => Encoding.UTF8.GetBytes(Key);

    /// <summary>Throws a descriptive error if the configuration is incomplete or insecure.</summary>
    public void EnsureValid()
    {
        if (string.IsNullOrWhiteSpace(Key) || GetKeyBytes().Length < MinKeyBytes)
        {
            throw new InvalidOperationException($"'Jwt:Key' must be configured and be at least {MinKeyBytes} bytes long.");
        }

        if (string.IsNullOrWhiteSpace(Issuer))
        {
            throw new InvalidOperationException("'Jwt:Issuer' is not configured.");
        }

        if (string.IsNullOrWhiteSpace(Audience))
        {
            throw new InvalidOperationException("'Jwt:Audience' is not configured.");
        }

        if (ExpiresMinutes <= 0)
        {
            throw new InvalidOperationException("'Jwt:ExpiresMinutes' must be greater than zero.");
        }
    }
}
