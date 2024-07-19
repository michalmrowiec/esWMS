using esMWS.Domain.Entities.Documents;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace esWMS.Infrastructure.EntitiesConfigurations.Documents
{
    internal class MMMConfiguration : IEntityTypeConfiguration<MMM>
    {
        public void Configure(EntityTypeBuilder<MMM> builder)
        {
            builder
                .HasOne(d => d.ToWarehouse)
                .WithMany(w => w.MMMDocuments)
                .HasForeignKey(d => d.ToWarehouseId)
                .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
