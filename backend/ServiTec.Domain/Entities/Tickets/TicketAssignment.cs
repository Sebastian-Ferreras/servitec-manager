using ServiTec.Domain.Common;

namespace ServiTec.Domain.Entities;

public class TicketAssignment : BaseEntity
{
    public Guid TicketId { get; set; }
    public Guid? PreviousTechnicianId { get; set; }
    public Guid NewTechnicianId { get; set; }
    public string? Reason { get; set; }
    public Guid AssignedByUserId { get; set; }

    public Ticket Ticket { get; set; } = null!;
}
