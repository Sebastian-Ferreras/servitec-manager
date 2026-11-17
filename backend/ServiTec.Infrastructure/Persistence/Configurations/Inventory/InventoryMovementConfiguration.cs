using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServiTec.Domain.Entities;

namespace ServiTec.Infrastructure.Persistence.Configurations;

public class InventoryMovementConfiguration : IEntityTypeConfiguration<InventoryMovement>
{
    public void Configure(EntityTypeBuilder<InventoryMovement> builder)
    {
        builder.ToTable("InventoryMovements", t =>
            t.HasCheckConstraint("CK_InventoryMovements_Quantity_Positive", "\"Quantity\" > 0"));
        builder.ConfigureBase();

        builder.Property(x => x.Type).HasConversion<string>().HasMaxLength(BaseEntityConfiguration.EnumMaxLength);
        builder.Property(x => x.Reason).IsRequired().HasMaxLength(BaseEntityConfiguration.LongTextMaxLength);
        builder.Property(x => x.Reference).HasMaxLength(100);
        builder.Property(x => x.IdempotencyKey).IsRequired().HasMaxLength(100);

        builder.HasOne(x => x.Part)
            .WithMany(p => p.Movements)
            .HasForeignKey(x => x.PartId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<InventoryMovement>()
            .WithMany()
            .HasForeignKey(x => x.RelatedMovementId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(x => x.PerformedByUserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => x.IdempotencyKey).IsUnique();
        builder.HasIndex(x => x.PartId);
    }
}
