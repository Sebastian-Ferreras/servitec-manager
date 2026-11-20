using ServiTec.Domain.Entities;

namespace Servitec.Application.Interfaces.Repositories;

public interface IClientRepository
{
    
    Task<Client?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<Client>> GetAllAsync(string? search, int page, int pageSize, CancellationToken cancellationToken = default);

    Task AddAsync(Client client, CancellationToken cancellationToken = default);
    Task UpdateAsync(Client client, CancellationToken cancellationToken = default);

    Task <bool> ExistsByEmailAsync(Guid id, CancellationToken cancellationToken = default); 
}