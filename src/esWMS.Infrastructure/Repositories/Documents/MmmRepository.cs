using esMWS.Domain.Entities.Documents;
using esWMS.Application.Contracts.Persistence.Documents;
using Microsoft.Extensions.Logging;
using Sieve.Services;

namespace esWMS.Infrastructure.Repositories.Documents
{
    internal class MmmRepository(EsWmsDbContext context, ILogger<MmmRepository> logger, ISieveProcessor sieveProcessor)
                : BaseDocumentRepository<MMM>(context, logger, sieveProcessor), IMmmRepository
    {
        private readonly EsWmsDbContext _context = context;
        private readonly ILogger<MmmRepository> _logger = logger;
        private readonly ISieveProcessor _sieveProcessor = sieveProcessor;
    }
}
