using esMWS.Domain.Entities.Documents;
using esWMS.Application.Contracts.Persistence.Documents;
using Microsoft.Extensions.Logging;
using Sieve.Services;

namespace esWMS.Infrastructure.Repositories.Documents
{
    internal class ZwRepository(EsWmsDbContext context, ILogger<ZwRepository> logger, ISieveProcessor sieveProcessor)
                : BaseDocumentRepository<ZW>(context, logger, sieveProcessor), IZwRepository
    {
        private readonly EsWmsDbContext _context = context;
        private readonly ILogger<ZwRepository> _logger = logger;
        private readonly ISieveProcessor _sieveProcessor = sieveProcessor;
    }
}
