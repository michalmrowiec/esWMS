using esMWS.Domain.Entities.WarehouseEnviroment;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;

namespace esWMS.Infrastructure.EntitiesConfigurations.WarehouseEnviroment
{
    internal class WarehouseUnitItemConfiguration : IEntityTypeConfiguration<WarehouseUnitItem>
    {
        public void Configure(EntityTypeBuilder<WarehouseUnitItem> builder)
        {
            builder.HasKey(wui => wui.WarehouseUnitItemId);

            builder.Property(wui => wui.WarehouseUnitItemId)
                .IsRequired()
                .HasMaxLength(450);

            builder.Property(wui => wui.WarehouseUnitId)
                .IsRequired()
                .HasMaxLength(450);

            builder.Property(wui => wui.ProductId)
                .IsRequired()
                .HasMaxLength(450);

            builder.Property(wui => wui.Quantity)
                .IsRequired()
                .HasDefaultValue(0);

            builder.Property(wui => wui.BatchLot)
                .HasMaxLength(50);

            builder.Property(wui => wui.SerialNumber)
                .HasMaxLength(100);

            builder.Property(wui => wui.Price)
                .HasColumnType("decimal(18,2)");

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
        }
    }
}
