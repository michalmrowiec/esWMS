using esMWS.Domain.Entities.Documents;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace esWMS.Infrastructure.EntitiesConfigurations.Documents
{
    internal class PZConfiguration : IEntityTypeConfiguration<PZ>
    {
        public void Configure(EntityTypeBuilder<PZ> builder)
        {
            builder
                .HasOne(d => d.SupplierContractor)
                .WithMany(c => c.PZDocuments)
                .HasForeignKey(d => d.SupplierContractorId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
