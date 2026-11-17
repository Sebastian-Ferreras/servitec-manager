using System.ComponentModel.DataAnnotations;

namespace ServiTec.Application.DTOs.Request.Auth;

public sealed record LoginRequest
{
    [Required, EmailAddress, MaxLength(254)]
    public string Email { get; init; } = string.Empty;

    [Required, MaxLength(128)]
    public string Password { get; init; } = string.Empty;
}
