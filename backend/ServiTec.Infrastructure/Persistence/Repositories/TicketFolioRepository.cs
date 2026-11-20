using Microsoft.EntityFrameworkCore;
using ServiTec.Application.Interfaces.Repositories;

namespace ServiTec.Infrastructure.Persistence.Repositories;

public class TicketFolioRepository : ITicketFolioRepository
{
    private readonly AppDbContext _context;
    public TicketFolioRepository(AppDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }
    public async Task<string> GenerateFolioAsync(CancellationToken cancellationToken)
    {
        long nextNumber = await _context.Database
            .SqlQuery<long>($"SELECT nextval('ticket_folio_seq') AS \"Value\"")
            .SingleAsync(cancellationToken);

        string formatted = nextNumber.ToString("D6");
        return $"TK-{formatted}";
    }
}