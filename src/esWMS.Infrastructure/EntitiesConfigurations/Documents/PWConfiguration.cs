using esWMS.Domain.Entities.Documents;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace esWMS.Infrastructure.EntitiesConfigurations.Documents
{
    internal class PwConfiguration : IEntityTypeConfiguration<PW>
    {
        public void Configure(EntityTypeBuilder<PW> builder)
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
