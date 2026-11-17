using ServiTec.Domain.Enums;

namespace ServiTec.Application.DTOs.Common.Auth;

public sealed record AuthUserDto(Guid Id, string FullName, string Email, UserRole Role);
