namespace ServiTec.Application.Interfaces.Repositories;

public interface ITicketFolioRepository
{
    Task<string> GenerateFolioAsync(CancellationToken cancellationToken);
}