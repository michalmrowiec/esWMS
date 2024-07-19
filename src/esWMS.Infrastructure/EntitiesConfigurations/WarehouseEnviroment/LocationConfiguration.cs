using esMWS.Domain.Entities.WarehouseEnviroment;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace esWMS.Infrastructure.EntitiesConfigurations.WarehouseEnviroment
{
    internal class LocationConfiguration : IEntityTypeConfiguration<Location>
    {
        public void Configure(EntityTypeBuilder<Location> builder)
        {
            builder.HasKey(l => l.LocationId);

            builder
                .HasOne(l => l.Zone)
                .WithMany(z => z.Locations)
                .HasForeignKey(l => l.ZoneId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(l => l.DefaultMediaType)
                .WithMany()
                .HasForeignKey(l => l.DefaultMediaTypeId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasMany(l => l.WarehouseUnits)
                .WithOne(wu => wu.Location)
                .HasForeignKey(wu => wu.LocationId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
