using esWMS.Domain.Entities.Documents;
using esWMS.Application.Contracts.Persistence.Documents;
using Microsoft.Extensions.Logging;
using Sieve.Services;

namespace esWMS.Infrastructure.Repositories.Documents
{
    internal class MmpRepository(EsWmsDbContext context, ILogger<MmpRepository> logger, ISieveProcessor sieveProcessor)
                : BaseDocumentRepository<MMP>(context, logger, sieveProcessor), IMmpRepository
    {
        private readonly EsWmsDbContext _context = context;
        private readonly ILogger<MmpRepository> _logger = logger;
        private readonly ISieveProcessor _sieveProcessor = sieveProcessor;
    }
}
