using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServiTec.Domain.Entities;

namespace ServiTec.Infrastructure.Persistence.Configurations;

public class TicketConfiguration : IEntityTypeConfiguration<Ticket>
{
    public void Configure(EntityTypeBuilder<Ticket> builder)
    {
        builder.ToTable("Tickets");
        builder.ConfigureBase();

        builder.Property(x => x.Folio).IsRequired().HasMaxLength(20);
        builder.Property(x => x.ReportedFault).IsRequired().HasMaxLength(BaseEntityConfiguration.LongTextMaxLength);
        builder.Property(x => x.Accessories).HasMaxLength(500);
        builder.Property(x => x.PhysicalCondition).IsRequired().HasMaxLength(BaseEntityConfiguration.LongTextMaxLength);
        builder.Property(x => x.Status).HasConversion<string>().HasMaxLength(BaseEntityConfiguration.EnumMaxLength);
        builder.Property(x => x.CancellationReason).HasMaxLength(BaseEntityConfiguration.LongTextMaxLength);

        builder.HasOne(x => x.Client)
            .WithMany(c => c.Tickets)
            .HasForeignKey(x => x.ClientId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Device)
            .WithMany()
            .HasForeignKey(x => x.DeviceId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(x => x.AssignedTechnicianId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(x => x.CreatedByUserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => x.Folio).IsUnique();
        builder.HasIndex(x => x.Status);
        builder.HasIndex(x => x.ClientId);
        builder.HasIndex(x => x.AssignedTechnicianId);
    }
}
