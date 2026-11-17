using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServiTec.Domain.Entities;

namespace ServiTec.Infrastructure.Persistence.Configurations;

public class DiagnosisConfiguration : IEntityTypeConfiguration<Diagnosis>
{
    public void Configure(EntityTypeBuilder<Diagnosis> builder)
    {
        builder.ToTable("Diagnoses");
        builder.ConfigureBase();

        builder.Property(x => x.Type).HasConversion<string>().HasMaxLength(BaseEntityConfiguration.EnumMaxLength);
        builder.Property(x => x.Content).IsRequired().HasMaxLength(BaseEntityConfiguration.LongTextMaxLength);

        builder.HasOne(x => x.Ticket)
            .WithMany(t => t.Diagnoses)
            .HasForeignKey(x => x.TicketId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(x => x.AuthorUserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
