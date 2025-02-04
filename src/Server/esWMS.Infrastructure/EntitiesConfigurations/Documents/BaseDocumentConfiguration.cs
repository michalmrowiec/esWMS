﻿using esWMS.Domain.Entities.Documents;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace esWMS.Infrastructure.EntitiesConfigurations.Documents
{
    public class BaseDocumentConfiguration : IEntityTypeConfiguration<BaseDocument>
    {
        public void Configure(EntityTypeBuilder<BaseDocument> builder)
        {
            builder.HasKey(d => d.DocumentId);

            builder.HasIndex(d => d.DocumentIssueDate)
                .HasDatabaseName("IX_Documents_DocumentIssueDate");

            builder.Property(d => d.DocumentId)
                .IsRequired()
                .HasMaxLength(25);

            builder.Property(d => d.IssueWarehouseId)
                .IsRequired()
                .HasMaxLength(3);

            builder.Property(d => d.DocumentIssueDate)
                .IsRequired();

            builder.Property(d => d.IsApproved)
                .IsRequired()
                .HasDefaultValue(false);

            builder.Property(d => d.CreatedAt)
                .IsRequired();

            builder.Property(d => d.CreatedBy)
                .HasMaxLength(60);

            builder.Property(d => d.ModifiedBy)
                .HasMaxLength(60);

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
