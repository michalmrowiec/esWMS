using esMWS.Domain.Entities.SystemActors;
using esWMS.Application.Contracts.Persistence;
using Microsoft.Extensions.Logging;

namespace esWMS.Infrastructure.Repositories
{
    internal class ContractorRepository(EsWmsDbContext context, ILogger<ContractorRepository> logger)
        : BaseRepository<Contractor>(context, logger), IContractorRepository
    { }
}
