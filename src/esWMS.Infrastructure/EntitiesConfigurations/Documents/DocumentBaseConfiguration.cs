using esMWS.Domain.Entities.Documents;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace esWMS.Infrastructure.EntitiesConfigurations.Documents
{
    internal class DocumentBaseConfiguration : IEntityTypeConfiguration<DocumentBase>
    {
        public void Configure(EntityTypeBuilder<DocumentBase> builder)
        {
            
        }
    }
}
