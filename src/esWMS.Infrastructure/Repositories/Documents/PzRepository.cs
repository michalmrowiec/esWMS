using esMWS.Domain.Entities.Documents;
using esMWS.Domain.Models;
using esWMS.Application.Contracts.Persistence.Documents;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sieve.Models;
using Sieve.Services;

namespace esWMS.Infrastructure.Repositories.Documents
{
    internal class PzRepository(EsWmsDbContext context, ILogger<PzRepository> logger, ISieveProcessor sieveProcessor)
                : BaseDocumentRepository<PZ>(context, logger, sieveProcessor), IPzRepository
    {
        private readonly EsWmsDbContext _context = context;
        private readonly ILogger<PzRepository> _logger = logger;
        private readonly ISieveProcessor _sieveProcessor = sieveProcessor;
    }
}
