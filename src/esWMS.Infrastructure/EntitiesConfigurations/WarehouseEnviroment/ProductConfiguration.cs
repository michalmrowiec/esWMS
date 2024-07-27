﻿using esMWS.Domain.Entities.WarehouseEnviroment;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace esWMS.Infrastructure.EntitiesConfigurations.WarehouseEnviroment
{
    internal class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.ProductId);

            builder.Property(p => p.ProductId)
               .IsRequired()
               .HasMaxLength(450);

            builder.Property(p => p.ProductCode)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(p => p.EanCode)
                .HasMaxLength(100);

            builder.Property(p => p.ProductName)
                .IsRequired()
                .HasMaxLength(250);

            builder.Property(p => p.CategoryId)
                .IsRequired()
                .HasMaxLength(450);

            builder.Property(p => p.Unit)
                .HasMaxLength(10);
            
            builder.Property(p => p.IsWeight)
                .IsRequired()
                .HasDefaultValue(false);

            builder.Property(p => p.IsMedia)
                .IsRequired()
                .HasDefaultValue(false);

            builder.Property(p => p.MediaTypeAlias)
                .HasMaxLength(10);

            builder.Property(p => p.Price)
                .HasColumnType("decimal(18,2)");

            builder.Property(p => p.IsActive)
                .IsRequired();

            builder.Property(p => p.CreatedAt)
                .IsRequired();

            builder.Property(p => p.CreatedBy)
                .HasMaxLength(60);

            builder.Property(p => p.ModifiedBy)
                .HasMaxLength(60);

            builder
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(p => p.SupplierContractor)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.SupplierContractorId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasMany(p => p.LocationDefaultMedia)
                .WithOne(l => l.DefaultMediaType)
                .HasForeignKey(l => l.DefaultMediaTypeId)
                .OnDelete(DeleteBehavior.SetNull);

            builder
                .HasMany(p => p.WarehouseUnitItems)
                .WithOne(wui => wui.Product)
                .HasForeignKey(wui => wui.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
