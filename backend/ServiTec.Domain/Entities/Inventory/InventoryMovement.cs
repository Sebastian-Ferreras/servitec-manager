using ServiTec.Domain.Common;
using ServiTec.Domain.Enums;

namespace ServiTec.Domain.Entities;

public class InventoryMovement : BaseEntity
{
    public Guid PartId { get; set; }
    public MovementType Type { get; set; }
    public int Quantity { get; set; }
    public string Reason { get; set; } = string.Empty;
    public string? Reference { get; set; }
    public Guid? RelatedMovementId { get; set; }
    public string IdempotencyKey { get; set; } = string.Empty;
    public Guid PerformedByUserId { get; set; }

    public Part Part { get; set; } = null!;
}
