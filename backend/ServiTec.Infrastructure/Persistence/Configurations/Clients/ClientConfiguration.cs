using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServiTec.Domain.Entities;

namespace ServiTec.Infrastructure.Persistence.Configurations;

public class ClientConfiguration : IEntityTypeConfiguration<Client>
{
    public void Configure(EntityTypeBuilder<Client> builder)
    {
        builder.ToTable("Clients");
        builder.ConfigureBase();

        builder.Property(x => x.FullName).IsRequired().HasMaxLength(150);
        builder.Property(x => x.Email).HasMaxLength(254);
        builder.Property(x => x.IsActive).IsRequired();
    }
}
