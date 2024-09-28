using esWMS.Domain.Entities.Documents;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace esWMS.Infrastructure.EntitiesConfigurations.Documents
{
    internal class MmpConfiguration : IEntityTypeConfiguration<MMP>
    {
        public void Configure(EntityTypeBuilder<MMP> builder)
        {
            builder.Property(d => d.FromWarehouseId)
                .IsRequired()
                .HasMaxLength(3);

            builder.Property(d => d.RelatedMmmId)
                .IsRequired()
                .HasMaxLength(25);

            builder.Property(d => d.GoodsReceiptDate)
                .HasColumnName("GoodsReceiptDate")
                .IsRequired(false);

            builder
                .HasOne(d => d.FromWarehouse)
                .WithMany(w => w.MMPDocuments)
                .HasForeignKey(d => d.FromWarehouseId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(d => d.RelatedMmm)
                .WithOne(d => d.RelatedMmp)
                .HasForeignKey<MMP>(d => d.RelatedMmmId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
