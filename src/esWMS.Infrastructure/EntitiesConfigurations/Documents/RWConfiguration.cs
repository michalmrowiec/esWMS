using esMWS.Domain.Entities.Documents;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace esWMS.Infrastructure.EntitiesConfigurations.Documents
{
    internal class RWConfiguration : IEntityTypeConfiguration<RW>
    {
        public void Configure(EntityTypeBuilder<RW> builder)
        {
            builder.Property(d => d.DepartmentName)
                .HasMaxLength(100);
        }
    }
}
