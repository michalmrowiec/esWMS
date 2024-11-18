using esWMS.Infrastructure.Utilities.SieveProcessorConfigurations.Documents;
using esWMS.Infrastructure.Utilities.SieveProcessorConfigurations.SystemActors;
using esWMS.Infrastructure.Utilities.SieveProcessorConfigurations.WarehouseEnviroment;
using Microsoft.Extensions.Options;
using Sieve.Models;
using Sieve.Services;

namespace esWMS.Infrastructure.Utilities.SieveProcessorConfigurations
{
    public class EsWmsSieveProcessor
        (IOptions<SieveOptions> options,
        ISieveCustomFilterMethods customFilterMethods,
        ISieveCustomSortMethods customSortMethods)
        : SieveProcessor(options, customSortMethods, customFilterMethods)
    {
        protected override SievePropertyMapper MapProperties(SievePropertyMapper mapper)
        {
            new ProductSieveProcessor().Configure(mapper);

            new CategorySieveProcessor().Configure(mapper);

            new ContractorSieveProcessor().Configure(mapper);

            new EmployeeSieveProcessor().Configure(mapper);

            new LocationSieveProcessor().Configure(mapper);

            new ZoneSieveProcessor().Configure(mapper);

            new WarehouseSieveProcessor().Configure(mapper);

            new WarehouseUnitSieveProcessor().Configure(mapper);

            new WarehouseUnitItemSieveProcessor().Configure(mapper);

            new WarehouseStockSieveProcessor().Configure(mapper);

            new DocumentItemsSieveProcessor().Configure(mapper);

            new PzSieveProcessor().Configure(mapper);

            new WzSieveProcessor().Configure(mapper);

            new MmmSieveProcessor().Configure(mapper);

            new MmpSieveProcessor().Configure(mapper);

            new PwSieveProcessor().Configure(mapper);

            new RwSieveProcessor().Configure(mapper);

            new ZwSieveProcessor().Configure(mapper);

            return mapper;
        }
    }
}
