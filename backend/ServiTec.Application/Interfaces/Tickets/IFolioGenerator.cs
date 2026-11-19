namespace ServiTec.Application.Interfaces.Tickets;

public interface IFolioGenerator
{
    Task<string> GenerateFolioAsync(CancellationToken cancellationToken);
}