using esMWS.Domain.Entities.Documents;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace esWMS.Infrastructure.EntitiesConfigurations.Documents
{
    internal class WzConfiguration : IEntityTypeConfiguration<WZ>
    {
        public void Configure(EntityTypeBuilder<WZ> builder)
        {
            builder.Property(d => d.GoodsReleaseDate)
                .HasColumnName("GoodsReleaseDate")
                .IsRequired(false);

            builder.Property(d => d.RecipientContractorId)
                .IsRequired()
                .HasMaxLength(3);

            builder
                .HasOne(d => d.RecipientContractor)
                .WithMany(c => c.WZDocuments)
                .HasForeignKey(d => d.RecipientContractorId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
