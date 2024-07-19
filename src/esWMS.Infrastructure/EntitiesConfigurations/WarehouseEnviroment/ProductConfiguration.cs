using esMWS.Domain.Entities.WarehouseEnviroment;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace esWMS.Infrastructure.EntitiesConfigurations.WarehouseEnviroment
{
    internal class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.ProductId);

            builder
                .HasOne(p => p.Category)
                .WithMany()
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
