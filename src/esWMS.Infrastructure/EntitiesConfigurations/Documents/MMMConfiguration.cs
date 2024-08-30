using esMWS.Domain.Entities.Documents;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace esWMS.Infrastructure.EntitiesConfigurations.Documents
{
    internal class MmmConfiguration : IEntityTypeConfiguration<MMM>
    {
        public void Configure(EntityTypeBuilder<MMM> builder)
        {
            builder.Property(d => d.ToWarehouseId)
                .IsRequired()
                .HasMaxLength(3);

            builder
                .HasOne(d => d.ToWarehouse)
                .WithMany(w => w.MMMDocuments)
                .HasForeignKey(d => d.ToWarehouseId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(d => d.RelatedMmp)
                .WithOne(d => d.RelatedMmm)
                .HasForeignKey<MMP>(d => d.RelatedMmmId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
