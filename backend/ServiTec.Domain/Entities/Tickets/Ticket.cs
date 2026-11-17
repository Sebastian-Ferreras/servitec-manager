using ServiTec.Domain.Common;
using ServiTec.Domain.Enums;

namespace ServiTec.Domain.Entities;

public class Ticket : BaseEntity
{
    public string Folio { get; set; } = string.Empty;
    public Guid ClientId { get; set; }
    public Guid DeviceId { get; set; }
    public string ReportedFault { get; set; } = string.Empty;
    public string? Accessories { get; set; }
    public string PhysicalCondition { get; set; } = string.Empty;
    public TicketStatus Status { get; set; } = TicketStatus.Received;
    public Guid? AssignedTechnicianId { get; set; }
    public Guid CreatedByUserId { get; set; }
    public string? CancellationReason { get; set; }

    public Client Client { get; set; } = null!;
    public Device Device { get; set; } = null!;
    public ICollection<TicketChangeLog> ChangeLogs { get; set; } = [];
    public ICollection<TicketStatusHistory> StatusHistory { get; set; } = [];
    public ICollection<TicketAssignment> Assignments { get; set; } = [];
    public ICollection<Diagnosis> Diagnoses { get; set; } = [];
    public ICollection<Quote> Quotes { get; set; } = [];
}
