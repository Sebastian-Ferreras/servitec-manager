using ServiTec.Domain.Common;
using ServiTec.Domain.Enums;

namespace ServiTec.Domain.Entities;

public class Quote : BaseEntity
{
    public Guid TicketId { get; set; }
    public int Version { get; set; }
    public QuoteStatus Status { get; set; } = QuoteStatus.Pending;
    public decimal Total { get; set; }
    public string? DecisionMethod { get; set; }
    public DateTime? DecisionAt { get; set; }
    public decimal? ApprovedAmount { get; set; }
    public string? RejectionReason { get; set; }
    public Guid CreatedByUserId { get; set; }

    public Ticket Ticket { get; set; } = null!;
    public ICollection<QuoteItem> Items { get; set; } = [];
}
