using esMWS.Domain.Entities.Documents;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace esWMS.Infrastructure.EntitiesConfigurations.Documents
{
    internal class PWConfiguration : IEntityTypeConfiguration<PW>
    {
        public void Configure(EntityTypeBuilder<PW> builder)
        {
            builder.Property(d => d.DepartmentName)
                .HasMaxLength(250);
        }
    }
}
