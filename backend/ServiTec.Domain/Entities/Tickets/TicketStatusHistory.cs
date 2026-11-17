using ServiTec.Domain.Common;
using ServiTec.Domain.Enums;

namespace ServiTec.Domain.Entities;

public class TicketStatusHistory : BaseEntity
{
    public Guid TicketId { get; set; }
    public TicketStatus? FromStatus { get; set; }
    public TicketStatus ToStatus { get; set; }
    public string? Reason { get; set; }
    public Guid ChangedByUserId { get; set; }

    public Ticket Ticket { get; set; } = null!;
}
