using ServiTec.Domain.Common;

namespace ServiTec.Domain.Entities;

public class TicketChangeLog : BaseEntity
{
    public Guid TicketId { get; set; }
    public string Field { get; set; } = string.Empty;
    public string? OldValue { get; set; }
    public string? NewValue { get; set; }
    public Guid ChangedByUserId { get; set; }

    public Ticket Ticket { get; set; } = null!;
}
