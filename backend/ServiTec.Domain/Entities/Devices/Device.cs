using ServiTec.Domain.Common;

namespace ServiTec.Domain.Entities;

public class Device : BaseEntity
{
    public Guid ClientId { get; set; }
    public string Brand { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public string? SerialNumber { get; set; }
    public string Location { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;

    public Client Client { get; set; } = null!;
}
