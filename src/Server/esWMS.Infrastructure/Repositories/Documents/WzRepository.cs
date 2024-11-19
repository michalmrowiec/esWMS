using esWMS.Domain.Entities.Documents;
using esWMS.Application.Contracts.Persistence;
using Microsoft.Extensions.Logging;
using Sieve.Services;

namespace esWMS.Infrastructure.Repositories.Documents
{
    internal class WzRepository(EsWmsDbContext context, ILogger<WzRepository> logger, ISieveProcessor sieveProcessor)
                : BaseDocumentRepository<WZ>(context, logger, sieveProcessor), IWzRepository
    {
        private readonly EsWmsDbContext _context = context;
        private readonly ILogger<WzRepository> _logger = logger;
        private readonly ISieveProcessor _sieveProcessor = sieveProcessor;
    }
}
