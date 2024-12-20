﻿using esWMS.Domain.Entities.Documents;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace esWMS.Infrastructure.EntitiesConfigurations.Documents
{
    public class DocumentItemConfiguration : IEntityTypeConfiguration<DocumentItem>
    {
        public void Configure(EntityTypeBuilder<DocumentItem> builder)
        {
            builder.HasKey(di => di.DocumentItemId);

            builder.Property(di => di.DocumentItemId)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(di => di.DocumentId)
                .IsRequired()
                .HasMaxLength(25);

            builder.Property(di => di.ProductId)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(di => di.ProductCode)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(di => di.EanCode)
                .HasMaxLength(100);

            builder.Property(di => di.ProductName)
                .IsRequired()
                .HasMaxLength(250);

            builder.Property(di => di.Quantity)
                .IsRequired()
                .HasDefaultValue(0);

            builder.Property(wui => wui.BatchLot)
                .HasMaxLength(50);

            builder.Property(wui => wui.SerialNumber)
                .HasMaxLength(100);

            builder.Property(di => di.Price)
                .HasColumnType("decimal(18,2)");

            builder.Property(di => di.Currency)
                .HasMaxLength(5);

            builder.Property(di => di.Unit)
                .HasMaxLength(10);

            builder.Property(di => di.IsApproved)
                .IsRequired();

            builder.Property(di => di.CreatedAt)
                .IsRequired();

            builder.Property(di => di.CreatedBy)
                .HasMaxLength(60);

            builder.Property(di => di.ModifiedBy)
                .HasMaxLength(60);

            builder
                .HasOne(di => di.Document)
                .WithMany(d => d.DocumentItems)
                .HasForeignKey(di => di.DocumentId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(di => di.Product)
                .WithMany(p => p.DocumentItems)
                .HasForeignKey(di => di.ProductId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasMany(di => di.DocumentWarehouseUnitItems)
                .WithOne(dwui => dwui.DocumentItem)
                .HasForeignKey(dwui => dwui.DocumentItemId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
