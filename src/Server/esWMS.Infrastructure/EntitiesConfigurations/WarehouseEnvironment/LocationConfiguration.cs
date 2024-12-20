﻿using esWMS.Domain.Entities.WarehouseEnvironment;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace esWMS.Infrastructure.EntitiesConfigurations.WarehouseEnvironment
{
    public class LocationConfiguration : IEntityTypeConfiguration<Location>
    {
        public void Configure(EntityTypeBuilder<Location> builder)
        {
            builder.HasKey(l => l.LocationId);

            builder.Property(l => l.LocationId)
                .IsRequired()
                .HasMaxLength(20);

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
                .HasDefaultValue(1m);

            builder.Property(l => l.MaxLength);

            builder.Property(l => l.MaxWidth);

            builder.Property(l => l.MaxHeight);

            builder.Property(l => l.MaxWeight);

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
