using ServiTec.Application.Interfaces;

namespace ServiTec.Infrastructure.Persistence;

public sealed class DatabaseHealthService(AppDbContext context) : IDatabaseHealthService
{
    private readonly AppDbContext _context = context;

    public Task<bool> CanConnectAsync(CancellationToken cancellationToken = default) =>
        _context.Database.CanConnectAsync(cancellationToken);
}
