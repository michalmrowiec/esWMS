using esMWS.Domain.Entities.Documents;
using esWMS.Application.Contracts.Persistence.Documents;
using Microsoft.Extensions.Logging;
using Sieve.Services;

namespace esWMS.Infrastructure.Repositories.Documents
{
    internal class PwRepository(EsWmsDbContext context, ILogger<PwRepository> logger, ISieveProcessor sieveProcessor)
                : BaseDocumentRepository<PW>(context, logger, sieveProcessor), IPwRepository
    {
        private readonly EsWmsDbContext _context = context;
        private readonly ILogger<PwRepository> _logger = logger;
        private readonly ISieveProcessor _sieveProcessor = sieveProcessor;
    }
}
