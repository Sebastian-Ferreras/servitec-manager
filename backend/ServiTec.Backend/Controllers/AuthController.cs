using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiTec.Application.DTOs.Request.Auth;
using ServiTec.Application.DTOs.Response.Auth;
using ServiTec.Application.Interfaces;

namespace ServiTec.Backend.Controllers;

[ApiController]
[Route("api/auth")]
public sealed class AuthController(IAuthService authService) : ControllerBase
{
    private readonly IAuthService _authService = authService;

    [HttpPost("login")]
    [AllowAnonymous]
    [ProducesResponseType<LoginResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<LoginResponse>> LoginAsync(LoginRequest request, CancellationToken cancellationToken) =>
        Ok(await _authService.LoginAsync(request, cancellationToken));

    [HttpGet("me")]
    [Authorize]
    [ProducesResponseType<CurrentUserResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<CurrentUserResponse>> GetCurrentUserAsync(CancellationToken cancellationToken) =>
        Ok(await _authService.GetCurrentUserAsync(cancellationToken));
}
