using esWMS.Domain.Entities.WarehouseEnvironment;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace esWMS.Infrastructure.EntitiesConfigurations.WarehouseEnvironment
{
    public class WarehouseConfiguration : IEntityTypeConfiguration<Warehouse>
    {
        public void Configure(EntityTypeBuilder<Warehouse> builder)
        {
            builder.HasKey(w => w.WarehouseId);

            builder.Property(w => w.WarehouseId)
                .IsRequired()
                .HasMaxLength(3);

            builder.Property(w => w.WarehouseName)
                .IsRequired()
                .HasMaxLength(250);

            builder.Property(w => w.Country)
                .HasMaxLength(100);

            builder.Property(w => w.City)
                .HasMaxLength(100);

            builder.Property(w => w.Region)
                .HasMaxLength(100);

            builder.Property(w => w.PostalCode)
                .HasMaxLength(25);

            builder.Property(w => w.Address)
                .HasMaxLength(250);

            builder.Property(w => w.IsActive)
                .IsRequired();

            builder.Property(w => w.CreatedAt)
                .IsRequired();

            builder.Property(w => w.CreatedBy)
                .HasMaxLength(60);

            builder.Property(w => w.ModifiedBy)
                .HasMaxLength(60);

            builder
                .HasMany(w => w.Documents)
                .WithOne(d => d.IssueWarehouse)
                .HasForeignKey(d => d.IssueWarehouseId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasMany(w => w.MMMDocuments)
                .WithOne(d => d.ToWarehouse)
                .HasForeignKey(d => d.ToWarehouseId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasMany(w => w.MMPDocuments)
                .WithOne(d => d.FromWarehouse)
                .HasForeignKey(d => d.FromWarehouseId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasMany(w => w.WarehouseUnits)
                .WithOne(wu => wu.Warehouse)
                .HasForeignKey(wu => wu.WarehouseId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasMany(w => w.Zones)
                .WithOne(z => z.Warehouse)
                .HasForeignKey(z => z.WarehouseId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
