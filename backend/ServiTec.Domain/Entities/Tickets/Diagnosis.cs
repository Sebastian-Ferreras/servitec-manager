using ServiTec.Domain.Common;
using ServiTec.Domain.Enums;

namespace ServiTec.Domain.Entities;

public class Diagnosis : BaseEntity
{
    public Guid TicketId { get; set; }
    public DiagnosisType Type { get; set; }
    public string Content { get; set; } = string.Empty;
    public Guid AuthorUserId { get; set; }

    public Ticket Ticket { get; set; } = null!;
}
