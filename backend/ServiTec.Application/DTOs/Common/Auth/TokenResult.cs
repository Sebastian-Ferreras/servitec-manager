namespace ServiTec.Application.DTOs.Common.Auth;

/// <summary>Signed access token and its UTC expiration.</summary>
public sealed record TokenResult(string Token, DateTime ExpiresAt);
