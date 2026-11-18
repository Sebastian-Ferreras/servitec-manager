using ServiTec.Application.DTOs.Common.Auth;
using ServiTec.Application.DTOs.Request.Auth;
using ServiTec.Application.DTOs.Response.Auth;
using ServiTec.Application.Interfaces;
using ServiTec.Application.Interfaces.Security;
using ServiTec.Application.Interfaces.Repositories;
using ServiTec.Domain.Exceptions;

namespace ServiTec.Application.Services;

public sealed class AuthService(
    IUserRepository userRepository,
    IPasswordHasher passwordHasher,
    ITokenService tokenService,
    ICurrentUserService currentUserService) : IAuthService
{
    private const string InvalidCredentialsMessage = "Invalid email or password.";

    private readonly IUserRepository _userRepository = userRepository;
    private readonly IPasswordHasher _passwordHasher = passwordHasher;
    private readonly ITokenService _tokenService = tokenService;
    private readonly ICurrentUserService _currentUserService = currentUserService;

    public async Task<LoginResponse> LoginAsync(LoginRequest request, CancellationToken cancellationToken = default)
    {
        var email = NormalizeEmail(request.Email);
        var user = await _userRepository.GetByEmailAsync(email, cancellationToken);

        if (user is null || !user.IsActive || !_passwordHasher.Verify(request.Password, user.PasswordHash))
        {
            throw new UnauthorizedException(InvalidCredentialsMessage);
        }

        var token = _tokenService.GenerateToken(user);

        return new LoginResponse(
            token.Token,
            token.ExpiresAt,
            new AuthUserDto(user.Id, user.FullName, user.Email, user.Role));
    }

    public async Task<CurrentUserResponse> GetCurrentUserAsync(CancellationToken cancellationToken = default)
    {
        if (!_currentUserService.IsAuthenticated || _currentUserService.UserId is not { } userId)
        {
            throw new UnauthorizedException("Authentication is required.");
        }

        var user = await _userRepository.GetByIdAsync(userId, cancellationToken);
        if (user is null || !user.IsActive)
        {
            throw new UnauthorizedException("The user no longer has access.");
        }

        return new CurrentUserResponse(user.Id, user.FullName, user.Email, user.Role);
    }

    /// <summary>Emails are stored lower-cased; normalize input the same way.</summary>
    public static string NormalizeEmail(string email) => email.Trim().ToLowerInvariant();
}
