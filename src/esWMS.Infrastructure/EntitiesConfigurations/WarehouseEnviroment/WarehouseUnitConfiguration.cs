using esWMS.Domain.Entities.WarehouseEnviroment;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace esWMS.Infrastructure.EntitiesConfigurations.WarehouseEnviroment
{
    internal class WarehouseUnitConfiguration : IEntityTypeConfiguration<WarehouseUnit>
    {
        public void Configure(EntityTypeBuilder<WarehouseUnit> builder)
        {
            builder.HasKey(wu => wu.WarehouseUnitId);

            builder.Property(wu => wu.WarehouseUnitId)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(wu => wu.WarehouseId)
                .IsRequired()
                .HasMaxLength(3);

            builder.Property(wu => wu.IsBlocked)
                .HasDefaultValue(false)
                .IsRequired();

            builder.Property(wu => wu.TotalLength)
                .HasColumnType("decimal(18, 4)");

            builder.Property(wu => wu.TotalWidth)
                .HasColumnType("decimal(18, 4)");

            builder.Property(wu => wu.TotalHeight)
                .HasColumnType("decimal(18, 4)");

            builder.Property(wu => wu.TotalWeight)
                .HasColumnType("decimal(18, 4)");

            builder.Property(wu => wu.CreatedAt)
                .IsRequired();

            builder.Property(wu => wu.CreatedBy)
                .HasMaxLength(60);

            builder.Property(wu => wu.ModifiedBy)
                .HasMaxLength(60);

            builder
                .HasOne(wu => wu.Warehouse)
                .WithMany(w => w.WarehouseUnits)
                .HasForeignKey(wu => wu.WarehouseId)
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
