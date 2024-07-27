using esMWS.Domain.Entities.Documents;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace esWMS.Infrastructure.EntitiesConfigurations.Documents
{
    internal class MMPConfiguration : IEntityTypeConfiguration<MMP>
    {
        public void Configure(EntityTypeBuilder<MMP> builder)
        {
            builder.Property(d => d.FromWarehouseId)
                .IsRequired()
                .HasMaxLength(3);

            builder
                .HasOne(d => d.FromWarehouse)
                .WithMany(w => w.MMPDocuments)
                .HasForeignKey(d => d.FromWarehouseId)
                .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
