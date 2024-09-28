using esWMS.Domain.Entities.Documents;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace esWMS.Infrastructure.EntitiesConfigurations.Documents
{
    internal class ZwConfiguration : IEntityTypeConfiguration<ZW>
    {
        public void Configure(EntityTypeBuilder<ZW> builder)
        {
            builder.Property(d => d.GoodsReceiptDate)
                .HasColumnName("GoodsReceiptDate")
                .IsRequired(false);

            builder.Property(d => d.DepartmentName)
                .HasColumnName("DepartmentName")
                .HasMaxLength(250);
        }
    }
}
