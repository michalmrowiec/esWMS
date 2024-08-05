using esMWS.Domain.Entities.Documents;
using esWMS.Application.Contracts.Persistence.Documents;
using Microsoft.Extensions.Logging;
using Sieve.Services;

namespace esWMS.Infrastructure.Repositories.Documents
{
    internal class PzRepository(EsWmsDbContext context, ILogger<PzRepository> logger, ISieveProcessor sieveProcessor)
                : BaseDocumentRepository<PZ>(context, logger, sieveProcessor), IPzRepository
    { }
}
