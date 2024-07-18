using esMWS.Domain.Entities.WarehouseEnviroment;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace esWMS.Infrastructure.EntitiesConfigurations.WarehouseEnviroment
{
    internal class WarehouseUnitItemConfiguration : IEntityTypeConfiguration<WarehouseUnitItem>
    {
        public void Configure(EntityTypeBuilder<WarehouseUnitItem> builder)
        {
            builder
                .HasOne(wui => wui.WarehouseUnit)
                .WithMany(wu => wu.WarehouseUnitItems)
                .HasForeignKey(wui => wui.WarehouseUnitId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(wui => wui.Product)
                .WithMany(p => p.WarehouseUnitItems)
                .HasForeignKey(wui => wui.ProductId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
