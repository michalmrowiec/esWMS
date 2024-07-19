using esMWS.Domain.Entities.SystemActors;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace esWMS.Infrastructure.EntitiesConfigurations.SystemActors
{
    internal class ContractorConfiguration : IEntityTypeConfiguration<Contractor>
    {
        public void Configure(EntityTypeBuilder<Contractor> builder)
        {
            builder.HasKey(c => c.ContractorId);

            builder
                .HasMany(c => c.PZDocuments)
                .WithOne(pz => pz.SupplierContractor)
                .HasForeignKey(pz => pz.SupplierContractorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasMany(c => c.WZDocuments)
                .WithOne(pz => pz.RecipientContractor)
                .HasForeignKey(pz => pz.RecipientContractorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasMany(c => c.Products)
                .WithOne(p => p.SupplierContractor)
                .HasForeignKey(p => p.SupplierContractorId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
