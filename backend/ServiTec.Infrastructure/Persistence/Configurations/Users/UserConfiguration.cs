using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServiTec.Domain.Entities;

namespace ServiTec.Infrastructure.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");
        builder.ConfigureBase();

        builder.Property(x => x.FullName).IsRequired().HasMaxLength(150);
        builder.Property(x => x.Email).IsRequired().HasMaxLength(254);
        builder.Property(x => x.PasswordHash).IsRequired().HasMaxLength(255);
        builder.Property(x => x.Role).HasConversion<string>().HasMaxLength(BaseEntityConfiguration.EnumMaxLength);
        builder.Property(x => x.IsActive).IsRequired();

        builder.HasIndex(x => x.Email).IsUnique();
    }
}
