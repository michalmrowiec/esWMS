using esMWS.Domain.Entities.Documents;
using esWMS.Application.Contracts.Persistence.Documents;
using Microsoft.Extensions.Logging;

namespace esWMS.Infrastructure.Repositories.Documents
{
    internal class PzRepository(EsWmsDbContext context, ILogger<PzRepository> logger)
                : BaseDocumentRepository<PZ>(context, logger), IPzRepozitory
    { }
}
