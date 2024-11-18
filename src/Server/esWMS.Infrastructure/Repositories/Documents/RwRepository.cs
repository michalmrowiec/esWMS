using esWMS.Domain.Entities.Documents;
using esWMS.Application.Contracts.Persistence.Documents;
using Microsoft.Extensions.Logging;
using Sieve.Services;

namespace esWMS.Infrastructure.Repositories.Documents
{
    internal class RwRepository(EsWmsDbContext context, ILogger<RwRepository> logger, ISieveProcessor sieveProcessor)
                : BaseDocumentRepository<RW>(context, logger, sieveProcessor), IRwRepository
    {
        private readonly EsWmsDbContext _context = context;
        private readonly ILogger<RwRepository> _logger = logger;
        private readonly ISieveProcessor _sieveProcessor = sieveProcessor;
    }
}
