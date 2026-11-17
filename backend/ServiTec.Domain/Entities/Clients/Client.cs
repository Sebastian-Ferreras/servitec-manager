using ServiTec.Domain.Common;

namespace ServiTec.Domain.Entities;

public class Client : BaseEntity
{
    public string FullName { get; set; } = string.Empty;
    public string? Email { get; set; }
    public bool IsActive { get; set; } = true;

    public ICollection<ClientPhone> Phones { get; set; } = [];
    public ICollection<Device> Devices { get; set; } = [];
    public ICollection<Ticket> Tickets { get; set; } = [];
}
