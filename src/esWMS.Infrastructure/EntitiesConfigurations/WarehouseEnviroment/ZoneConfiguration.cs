using esMWS.Domain.Entities.WarehouseEnviroment;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace esWMS.Infrastructure.EntitiesConfigurations.WarehouseEnviroment
{
    internal class ZoneConfiguration : IEntityTypeConfiguration<Zone>
    {
        public void Configure(EntityTypeBuilder<Zone> builder)
        {
            builder.HasKey(z => z.ZoneId);

            builder
                .HasOne(z => z.Warehouse)
                .WithMany(w => w.Zones)
                .HasForeignKey(z => z.WarehouseId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasMany(z => z.Locations)
                .WithOne(l => l.Zone)
                .HasForeignKey(l => l.ZoneId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
