using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServiTec.Domain.Entities;

namespace ServiTec.Infrastructure.Persistence.Configurations;

public class QuoteConfiguration : IEntityTypeConfiguration<Quote>
{
    public void Configure(EntityTypeBuilder<Quote> builder)
    {
        builder.ToTable("Quotes");
        builder.ConfigureBase();

        builder.Property(x => x.Status).HasConversion<string>().HasMaxLength(BaseEntityConfiguration.EnumMaxLength);
        builder.Property(x => x.Total).HasPrecision(18, 2);
        builder.Property(x => x.DecisionMethod).HasMaxLength(50);
        builder.Property(x => x.ApprovedAmount).HasPrecision(18, 2);
        builder.Property(x => x.RejectionReason).HasMaxLength(BaseEntityConfiguration.LongTextMaxLength);

        builder.HasOne(x => x.Ticket)
            .WithMany(t => t.Quotes)
            .HasForeignKey(x => x.TicketId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(x => x.CreatedByUserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => new { x.TicketId, x.Version }).IsUnique();
    }
}
