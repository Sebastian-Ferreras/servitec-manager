using ServiTec.Domain.Enums;

namespace ServiTec.Application.DTOs.Response.Auth;

public sealed record CurrentUserResponse(Guid Id, string FullName, string Email, UserRole Role);
