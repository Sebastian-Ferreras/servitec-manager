using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServiTec.Domain.Entities;

namespace ServiTec.Infrastructure.Persistence.Configurations;

public class PartConfiguration : IEntityTypeConfiguration<Part>
{
    public void Configure(EntityTypeBuilder<Part> builder)
    {
        builder.ToTable("Parts", t =>
        {
            t.HasCheckConstraint("CK_Parts_Stock_NonNegative", "\"Stock\" >= 0");
            t.HasCheckConstraint("CK_Parts_MinimumStock_NonNegative", "\"MinimumStock\" >= 0");
        });
        builder.ConfigureBase();

        builder.Property(x => x.Code).IsRequired().HasMaxLength(50);
        builder.Property(x => x.Name).IsRequired().HasMaxLength(150);
        builder.Property(x => x.Category).IsRequired().HasMaxLength(100);
        builder.Property(x => x.ReferencePrice).HasPrecision(18, 2);
        builder.Property(x => x.IsActive).IsRequired();

        builder.HasIndex(x => x.Code).IsUnique();
    }
}
