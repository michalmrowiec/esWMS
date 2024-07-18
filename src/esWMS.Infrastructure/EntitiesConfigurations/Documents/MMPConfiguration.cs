using esMWS.Domain.Entities.Documents;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace esWMS.Infrastructure.EntitiesConfigurations.Documents
{
    internal class MMPConfiguration : IEntityTypeConfiguration<MMP>
    {
        public void Configure(EntityTypeBuilder<MMP> builder)
        {
            builder
                .HasOne(d => d.FromWarehouse)
                .WithMany(w => w.Documents as IList<MMP>)
                .HasForeignKey(d => d.FromWarehouseId)
                .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
