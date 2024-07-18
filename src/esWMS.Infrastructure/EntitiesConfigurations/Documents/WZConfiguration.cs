using esMWS.Domain.Entities.Documents;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace esWMS.Infrastructure.EntitiesConfigurations.Documents
{
    internal class WZConfiguration : IEntityTypeConfiguration<WZ>
    {
        public void Configure(EntityTypeBuilder<WZ> builder)
        {
            builder
                .HasOne(d => d.RecipientContractor)
                .WithMany(c => c.WZDocuments)
                .HasForeignKey(d => d.RecipientContractorId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
