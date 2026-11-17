using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServiTec.Domain.Entities;

namespace ServiTec.Infrastructure.Persistence.Configurations;

public class TicketStatusHistoryConfiguration : IEntityTypeConfiguration<TicketStatusHistory>
{
    public void Configure(EntityTypeBuilder<TicketStatusHistory> builder)
    {
        builder.ToTable("TicketStatusHistories");
        builder.ConfigureBase();

        builder.Property(x => x.FromStatus).HasConversion<string>().HasMaxLength(BaseEntityConfiguration.EnumMaxLength);
        builder.Property(x => x.ToStatus).HasConversion<string>().HasMaxLength(BaseEntityConfiguration.EnumMaxLength);
        builder.Property(x => x.Reason).HasMaxLength(BaseEntityConfiguration.LongTextMaxLength);

        builder.HasOne(x => x.Ticket)
            .WithMany(t => t.StatusHistory)
            .HasForeignKey(x => x.TicketId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(x => x.ChangedByUserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
