using ServiTec.Domain.Common;

namespace ServiTec.Domain.Entities;

public class QuoteItem : BaseEntity
{
    public Guid QuoteId { get; set; }
    public string Description { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public Guid? PartId { get; set; }

    public Quote Quote { get; set; } = null!;
}
