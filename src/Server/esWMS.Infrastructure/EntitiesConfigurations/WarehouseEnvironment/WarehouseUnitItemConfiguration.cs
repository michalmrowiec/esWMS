using esWMS.Domain.Entities.WarehouseEnvironment;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace esWMS.Infrastructure.EntitiesConfigurations.WarehouseEnvironment
{
    public class WarehouseUnitItemConfiguration : IEntityTypeConfiguration<WarehouseUnitItem>
    {
        public void Configure(EntityTypeBuilder<WarehouseUnitItem> builder)
        {
            builder.HasKey(wui => wui.WarehouseUnitItemId);

            builder.Property(wui => wui.WarehouseUnitItemId)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(wui => wui.WarehouseUnitId)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(wui => wui.ProductId)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(wui => wui.IsMediaOfWarehouseUnit)
                .IsRequired()
                .HasDefaultValue(false);

            builder.Property(wui => wui.Quantity)
                .IsRequired()
                .HasDefaultValue(0);

            builder.Property(wui => wui.BlockedQuantity)
                .IsRequired()
                .HasDefaultValue(0);

            builder.Property(di => di.Unit)
                .HasMaxLength(10);

            builder.Property(wui => wui.BatchLot)
                .HasMaxLength(50);

            builder.Property(wui => wui.SerialNumber)
                .HasMaxLength(100);

            builder.Property(wui => wui.Price)
                .HasColumnType("decimal(18,2)");

            builder.Property(wui => wui.Currency)
                .HasMaxLength(5);

            builder.Property(wui => wui.CreatedAt)
                .IsRequired();

            builder.Property(wui => wui.CreatedBy)
                .HasMaxLength(60);

            builder.Property(wui => wui.ModifiedBy)
                .HasMaxLength(60);

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

            builder
                .HasMany(wui => wui.DocumentWarehouseUnitItems)
                .WithOne(dwui => dwui.WarehouseUnitItem)
                .HasForeignKey(dwui => dwui.WarehouseUnitItemId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
