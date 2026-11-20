using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServiTec.Domain.Common;

namespace ServiTec.Infrastructure.Persistence.Configurations;

internal static class BaseEntityConfiguration
{
    public const int EnumMaxLength = 30;
    public const int LongTextMaxLength = TextLimits.LongTextMaxLength;

    public static void ConfigureBase<T>(this EntityTypeBuilder<T> builder) where T : BaseEntity
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedNever();
        builder.Property(x => x.CreatedAt).IsRequired();
    }
}
