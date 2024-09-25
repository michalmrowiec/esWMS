using esWMS.Domain.Entities.WarehouseEnviroment;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace esWMS.Infrastructure.EntitiesConfigurations.WarehouseEnviroment
{
    internal class ZoneConfiguration : IEntityTypeConfiguration<Zone>
    {
        public void Configure(EntityTypeBuilder<Zone> builder)
        {
            builder.HasKey(z => z.ZoneId);

            builder.Property(z => z.ZoneId)
                .IsRequired()
                .HasMaxLength(5);

            builder.Property(z => z.ZoneName)
                .HasMaxLength(30);

            builder.Property(z => z.ZoneAlias)
                .IsRequired()
                .HasMaxLength(1);

            builder.Property(z => z.WarehouseId)
                .IsRequired()
                .HasMaxLength(3);

            builder.Property(l => l.AvgTemperature)
                .HasColumnType("decimal(18, 2)");

            builder.Property(z => z.CreatedAt)
                .IsRequired();

            builder.Property(z => z.CreatedBy)
                .HasMaxLength(60);

            builder.Property(z => z.ModifiedBy)
                .HasMaxLength(60);

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
