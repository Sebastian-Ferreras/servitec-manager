using ServiTec.Application.DTOs.Common.Auth;

namespace ServiTec.Application.DTOs.Response.Auth;

public sealed record LoginResponse(string Token, DateTime ExpiresAt, AuthUserDto User);
