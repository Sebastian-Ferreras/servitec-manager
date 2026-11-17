using ServiTec.Domain.Common;

namespace ServiTec.Domain.Entities;

public class Part : BaseEntity
{
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public int Stock { get; set; }
    public int MinimumStock { get; set; }
    public decimal ReferencePrice { get; set; }
    public bool IsActive { get; set; } = true;

    public ICollection<InventoryMovement> Movements { get; set; } = [];
}
