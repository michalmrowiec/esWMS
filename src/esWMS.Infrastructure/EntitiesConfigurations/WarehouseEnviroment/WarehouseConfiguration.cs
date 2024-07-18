using esMWS.Domain.Entities.Documents;
using esMWS.Domain.Entities.WarehouseEnviroment;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace esWMS.Infrastructure.EntitiesConfigurations.WarehouseEnviroment
{
    internal class WarehouseConfiguration : IEntityTypeConfiguration<Warehouse>
    {
        public void Configure(EntityTypeBuilder<Warehouse> builder)
        {
            builder
                .HasMany(w => w.Documents)
                .WithOne(d => d.IssueWarehouse)
                .HasForeignKey(d => d.IssueWarehouseId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasMany(w => w.Documents as IList<MMM>)
                .WithOne(d => d.ToWarehouse)
                .HasForeignKey(d => d.ToWarehouseId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasMany(w => w.Documents as IList<MMP>)
                .WithOne(d => d.FromWarehouse)
                .HasForeignKey(d => d.FromWarehouseId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasMany(w => w.WarehouseUnits)
                .WithOne(wu => wu.Warehouse)
                .HasForeignKey(wu => wu.WarehouseId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasMany(w => w.Zones)
                .WithOne(z => z.Warehouse)
                .HasForeignKey(z => z.WarehouseId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
