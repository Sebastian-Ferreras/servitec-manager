using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServiTec.Domain.Entities;

namespace ServiTec.Infrastructure.Persistence.Configurations;

public class ClientPhoneConfiguration : IEntityTypeConfiguration<ClientPhone>
{
    public void Configure(EntityTypeBuilder<ClientPhone> builder)
    {
        builder.ToTable("ClientPhones");
        builder.ConfigureBase();

        builder.Property(x => x.PhoneNumber).IsRequired().HasMaxLength(20);
        builder.Property(x => x.IsPrimary).IsRequired();

        builder.HasOne(x => x.Client)
            .WithMany(c => c.Phones)
            .HasForeignKey(x => x.ClientId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => x.ClientId);
    }
}
