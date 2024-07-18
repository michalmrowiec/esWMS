using esMWS.Domain.Entities.WarehouseEnviroment;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace esWMS.Infrastructure.EntitiesConfigurations.WarehouseEnviroment
{
    internal class WarehouseUnitConfiguration : IEntityTypeConfiguration<WarehouseUnit>
    {
        public void Configure(EntityTypeBuilder<WarehouseUnit> builder)
        {
            builder
                .HasOne(wu => wu.Warehouse)
                .WithMany(w => w.WarehouseUnits)
                .HasForeignKey(wu => wu.WarehouseId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(wu => wu.Media)
                .WithMany()
                .HasForeignKey(wu => wu.MediaId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(wu => wu.Location)
                .WithMany(l => l.WarehouseUnits)
                .HasForeignKey(wu => wu.LocationId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(wu => wu.StackOn)
                .WithOne()
                .HasForeignKey<WarehouseUnit>(wu => wu.StackOnId)
                .OnDelete(DeleteBehavior.SetNull);

            builder
                .HasMany(wu => wu.WarehouseUnitItems)
                .WithOne(wui => wui.WarehouseUnit)
                .HasForeignKey(wui => wui.WarehouseUnitId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
