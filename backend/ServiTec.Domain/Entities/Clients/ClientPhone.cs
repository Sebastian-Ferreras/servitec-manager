using ServiTec.Domain.Common;

namespace ServiTec.Domain.Entities;

public class ClientPhone : BaseEntity
{
    public Guid ClientId { get; set; }
    public string PhoneNumber { get; set; } = string.Empty;
    public bool IsPrimary { get; set; }

    public Client Client { get; set; } = null!;
}
