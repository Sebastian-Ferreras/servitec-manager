using ServiTec.Application.DTOs.Request.Auth;
using ServiTec.Application.DTOs.Response.Auth;

namespace ServiTec.Application.Interfaces;

public interface IAuthService
{
    Task<LoginResponse> LoginAsync(LoginRequest request, CancellationToken cancellationToken = default);
    Task<CurrentUserResponse> GetCurrentUserAsync(CancellationToken cancellationToken = default);
}
