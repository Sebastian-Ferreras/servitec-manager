using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServiTec.Domain.Entities;

namespace ServiTec.Infrastructure.Persistence.Configurations;

public class TicketAssignmentConfiguration : IEntityTypeConfiguration<TicketAssignment>
{
    public void Configure(EntityTypeBuilder<TicketAssignment> builder)
    {
        builder.ToTable("TicketAssignments");
        builder.ConfigureBase();

        builder.Property(x => x.Reason).HasMaxLength(BaseEntityConfiguration.LongTextMaxLength);

        builder.HasOne(x => x.Ticket)
            .WithMany(t => t.Assignments)
            .HasForeignKey(x => x.TicketId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(x => x.PreviousTechnicianId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(x => x.NewTechnicianId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(x => x.AssignedByUserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
