using ServiTec.Domain.Entities;

namespace ServiTec.Application.Interfaces.Repositories;

public interface IDeliveryRepository
{
    

    Task<Device?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<Device>> GetByClientIdAsync(Guid clientId, CancellationToken cancellationToken = default);

    Task AddAsync(Device device, CancellationToken cancellationToken = default);
    Task UpdateAsync(Device device, CancellationToken cancellationToken = default);

    Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default);
    Task<bool> SerialNumberExistsAsync(string serialNumber, Guid? excludeDeviceId, CancellationToken cancellationToken = default);
}