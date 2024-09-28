using esWMS.Domain.Entities.Documents;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace esWMS.Infrastructure.EntitiesConfigurations.Documents
{
    internal class PzConfiguration : IEntityTypeConfiguration<PZ>
    {
        public void Configure(EntityTypeBuilder<PZ> builder)
        {
            builder.Property(d => d.SupplierContractorId)
                .IsRequired()
                .HasMaxLength(3);

            builder.Property(d => d.GoodsReceiptDate)
                .HasColumnName("GoodsReceiptDate")
                .IsRequired(false);

            builder
                .HasOne(d => d.SupplierContractor)
                .WithMany(c => c.PZDocuments)
                .HasForeignKey(d => d.SupplierContractorId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
