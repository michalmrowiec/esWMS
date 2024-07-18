using esMWS.Domain.Entities.Documents;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace esWMS.Infrastructure.EntitiesConfigurations.Documents
{
    internal class DocumentBaseConfiguration : IEntityTypeConfiguration<DocumentBase>
    {
        public void Configure(EntityTypeBuilder<DocumentBase> builder)
        {
            builder
                .HasDiscriminator<string>("DocumentType")
                .HasValue<PZ>("PZ")
                .HasValue<PW>("PW")
                .HasValue<ZW>("ZW")
                .HasValue<WZ>("WZ")
                .HasValue<RW>("RW")
                .HasValue<MMM>("MMM")
                .HasValue<MMP>("MMP");

            builder
                .HasOne(d => d.IssueWarehouse)
                .WithMany(w => w.Documents)
                .HasForeignKey(d => d.IssueWarehouseId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasMany(d => d.DocumentItems)
                .WithOne(di => di.Document)
                .HasForeignKey(di => di.DocumentId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
