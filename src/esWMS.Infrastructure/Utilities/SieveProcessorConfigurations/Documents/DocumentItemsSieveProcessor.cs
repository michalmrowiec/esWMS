using esMWS.Domain.Entities.Documents;
using Sieve.Services;

namespace esWMS.Infrastructure.Utilities.SieveProcessorConfigurations.Documents
{
    internal class DocumentItemsSieveProcessor : ISieveConfiguration
    {
        public void Configure(SievePropertyMapper mapper)
        {
            mapper.Property<DocumentItem>(x => x.DocumentItemId)
                .CanSort()
                .CanFilter();
            mapper.Property<DocumentItem>(x => x.DocumentId)
                .CanSort()
                .CanFilter();
            mapper.Property<DocumentItem>(x => x.ProductId)
                .CanSort()
                .CanFilter();
            mapper.Property<DocumentItem>(x => x.ProductCode)
                .CanSort()
                .CanFilter();
            mapper.Property<DocumentItem>(x => x.ProductName)
                .CanSort()
                .CanFilter();
            mapper.Property<DocumentItem>(x => x.EanCode)
                .CanSort()
                .CanFilter();
            mapper.Property<DocumentItem>(x => x.BatchLot)
                .CanSort()
                .CanFilter();
            mapper.Property<DocumentItem>(x => x.SerialNumber)
                .CanSort()
                .CanFilter();
            mapper.Property<DocumentItem>(x => x.BestBefore) //?
                .CanSort()
                .CanFilter();
            mapper.Property<DocumentItem>(x => x.Quantity)
                .CanSort()
                .CanFilter();
            mapper.Property<DocumentItem>(x => x.Price)
                .CanSort()
                .CanFilter();
        }
    }
}
