using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServiTec.Domain.Entities;

namespace ServiTec.Infrastructure.Persistence.Configurations;

public class TicketChangeLogConfiguration : IEntityTypeConfiguration<TicketChangeLog>
{
    public void Configure(EntityTypeBuilder<TicketChangeLog> builder)
    {
        builder.ToTable("TicketChangeLogs");
        builder.ConfigureBase();

        builder.Property(x => x.Field).IsRequired().HasMaxLength(100);
        builder.Property(x => x.OldValue).HasMaxLength(BaseEntityConfiguration.LongTextMaxLength);
        builder.Property(x => x.NewValue).HasMaxLength(BaseEntityConfiguration.LongTextMaxLength);

        builder.HasOne(x => x.Ticket)
            .WithMany(t => t.ChangeLogs)
            .HasForeignKey(x => x.TicketId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(x => x.ChangedByUserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
