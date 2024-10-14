using esWMS.Domain.Entities.Documents;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace esWMS.Infrastructure.EntitiesConfigurations.Documents
{
    internal class DocumentWarehouseUnitItemConfiguration : IEntityTypeConfiguration<DocumentWarehouseUnitItem>
    {
        public void Configure(EntityTypeBuilder<DocumentWarehouseUnitItem> builder)
        {
            builder.Property(dwui => dwui.DocumentItemId)
                .HasMaxLength(50);

            builder.Property(dwui => dwui.WarehouseUnitItemId)
                .HasMaxLength(50);

            builder.HasKey(dwui => new { dwui.DocumentItemId, dwui.WarehouseUnitItemId });

            builder
                .HasOne(dwui => dwui.DocumentItem)
                .WithMany(di => di.DocumentWarehouseUnitItems)
                .HasForeignKey(dwui => dwui.DocumentItemId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(dwui => dwui.WarehouseUnitItem)
                .WithMany(wui => wui.DocumentWarehouseUnitItems)
                .HasForeignKey(dwui => dwui.WarehouseUnitItemId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
