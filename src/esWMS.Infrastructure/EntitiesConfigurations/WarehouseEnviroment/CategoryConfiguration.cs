using esWMS.Domain.Entities.WarehouseEnviroment;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace esWMS.Infrastructure.EntitiesConfigurations.WarehouseEnviroment
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(c => c.CategoryId);

            builder.Property(c => c.CategoryId)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(c => c.CategoryName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(c => c.ParentCategoryId)
                .HasMaxLength(50);

            builder.Property(c => c.CreatedAt)
                .IsRequired();

            builder.Property(c => c.CreatedBy)
                .HasMaxLength(60);

            builder.Property(c => c.ModifiedBy)
                .HasMaxLength(60);

            builder
                .HasOne(c => c.ParentCategory)
                .WithMany(c => c.ChildCategories)
                .HasForeignKey(c => c.ParentCategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasMany(c => c.Products)
                .WithOne(p => p.Category)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
