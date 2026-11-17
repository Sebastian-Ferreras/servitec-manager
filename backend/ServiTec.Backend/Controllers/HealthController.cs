using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiTec.Application.Interfaces;

namespace ServiTec.Backend.Controllers;

[ApiController]
[Route("api/health")]
[AllowAnonymous]
public sealed class HealthController(IDatabaseHealthService databaseHealthService) : ControllerBase
{
    private readonly IDatabaseHealthService _databaseHealthService = databaseHealthService;

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
    public async Task<IActionResult> GetAsync(CancellationToken cancellationToken) =>
        await _databaseHealthService.CanConnectAsync(cancellationToken)
            ? Ok(new { status = "ok" })
            : StatusCode(StatusCodes.Status503ServiceUnavailable, new { status = "unavailable" });
}
