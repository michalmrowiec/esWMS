using esWMS.Domain.Entities.Documents;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace esWMS.Infrastructure.EntitiesConfigurations.Documents
{
    internal class RwConfiguration : IEntityTypeConfiguration<RW>
    {
        public void Configure(EntityTypeBuilder<RW> builder)
        {
            builder.Property(d => d.GoodsReleaseDate)
                .HasColumnName("GoodsReleaseDate")
                .IsRequired(false);

            builder.Property(d => d.DepartmentName)
                .HasColumnName("DepartmentName")
                .HasMaxLength(250);
        }
    }
}
