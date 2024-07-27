using esMWS.Domain.Entities.WarehouseEnviroment;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;

namespace esWMS.Infrastructure.EntitiesConfigurations.WarehouseEnviroment
{
    internal class LocationConfiguration : IEntityTypeConfiguration<Location>
    {
        public void Configure(EntityTypeBuilder<Location> builder)
        {
            builder.HasKey(l => l.LocationId);

            builder.Property(l => l.LocationId)
                .IsRequired()
                .HasMaxLength(11);

            builder.Property(l => l.ZoneId)
                .IsRequired()
                .HasMaxLength(5);

            builder.Property(l => l.Row)
                .IsRequired();

            builder.Property(l => l.Column)
                .IsRequired()
                .HasMaxLength(1);

            builder.Property(l => l.Level)
                .IsRequired();

            builder.Property(l => l.Cell)
                .IsRequired();

            builder.Property(l => l.Capacity)
                .IsRequired()
                .HasDefaultValue(1);

            builder.Property(l => l.IsBusy)
                .IsRequired()
                .HasDefaultValue(false);

            builder.Property(l => l.CreatedAt)
                .IsRequired();

            builder.Property(l => l.CreatedBy)
                .HasMaxLength(60);

            builder.Property(l => l.ModifiedBy)
                .HasMaxLength(60);

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
