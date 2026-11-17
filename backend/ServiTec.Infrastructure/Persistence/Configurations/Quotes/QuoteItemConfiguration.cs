using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServiTec.Domain.Entities;

namespace ServiTec.Infrastructure.Persistence.Configurations;

public class QuoteItemConfiguration : IEntityTypeConfiguration<QuoteItem>
{
    public void Configure(EntityTypeBuilder<QuoteItem> builder)
    {
        builder.ToTable("QuoteItems", t =>
        {
            t.HasCheckConstraint("CK_QuoteItems_Quantity_Positive", "\"Quantity\" > 0");
            t.HasCheckConstraint("CK_QuoteItems_UnitPrice_NonNegative", "\"UnitPrice\" >= 0");
        });
        builder.ConfigureBase();

        builder.Property(x => x.Description).IsRequired().HasMaxLength(500);
        builder.Property(x => x.UnitPrice).HasPrecision(18, 2);

        builder.HasOne(x => x.Quote)
            .WithMany(q => q.Items)
            .HasForeignKey(x => x.QuoteId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<Part>()
            .WithMany()
            .HasForeignKey(x => x.PartId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
