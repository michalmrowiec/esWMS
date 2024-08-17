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

            builder.Property(c => c.ContractorId)
                .IsRequired()
                .HasMaxLength(3);

            builder.Property(c => c.ContractorName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(c => c.VatId)
                .HasMaxLength(30);

            builder.Property(c => c.IsSupplier)
                .IsRequired();

            builder.Property(c => c.IsRecipient)
                .IsRequired();

            builder.Property(c => c.Country)
                .HasMaxLength(100);

            builder.Property(c => c.City)
                .HasMaxLength(100);

            builder.Property(c => c.Region)
                .HasMaxLength(100);

            builder.Property(c => c.PostalCode)
                .HasMaxLength(25);

            builder.Property(c => c.Address)
                .HasMaxLength(250);

            builder.Property(c => c.EmailAddress)
                .HasMaxLength(255);

            builder.Property(c => c.PhoneNumber)
                .HasMaxLength(20);

            builder.Property(c => c.IsActive)
                .IsRequired();

            builder.Property(c => c.CreatedAt)
                .IsRequired();

            builder.Property(c => c.CreatedBy)
                .HasMaxLength(60);

            builder.Property(c => c.ModifiedBy)
                .HasMaxLength(60);

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
