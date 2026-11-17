using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServiTec.Domain.Entities;

namespace ServiTec.Infrastructure.Persistence.Configurations;

public class DeviceConfiguration : IEntityTypeConfiguration<Device>
{
    public void Configure(EntityTypeBuilder<Device> builder)
    {
        builder.ToTable("Devices");
        builder.ConfigureBase();

        builder.Property(x => x.Brand).IsRequired().HasMaxLength(100);
        builder.Property(x => x.Model).IsRequired().HasMaxLength(100);
        builder.Property(x => x.SerialNumber).HasMaxLength(100);
        builder.Property(x => x.Location).IsRequired().HasMaxLength(150);
        builder.Property(x => x.IsActive).IsRequired();

        builder.HasOne(x => x.Client)
            .WithMany(c => c.Devices)
            .HasForeignKey(x => x.ClientId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => x.ClientId);
        builder.HasIndex(x => x.SerialNumber)
            .IsUnique()
            .HasFilter("\"SerialNumber\" IS NOT NULL");
    }
}
