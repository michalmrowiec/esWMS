using esMWS.Domain.Entities.Documents;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace esWMS.Infrastructure.EntitiesConfigurations.Documents
{
    internal class DocumentItemConfiguration : IEntityTypeConfiguration<DocumentItem>
    {
        public void Configure(EntityTypeBuilder<DocumentItem> builder)
        {
            builder
                .HasOne(di => di.Document)
                .WithMany(d => d.DocumentItems)
                .HasForeignKey(di => di.DocumentId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(di => di.Product)
                .WithMany(p => p.DocumentItems)
                .HasForeignKey(di => di.ProductId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
