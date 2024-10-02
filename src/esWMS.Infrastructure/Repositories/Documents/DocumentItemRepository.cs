using esWMS.Application.Contracts.Persistence.Documents;
using esWMS.Domain.Entities.Documents;
using Microsoft.Extensions.Logging;

namespace esWMS.Infrastructure.Repositories.Documents
{
    internal class DocumentItemRepository(
        EsWmsDbContext context,
        ILogger<DocumentItemRepository> logger)
        : BaseRepository<DocumentItem>(context, logger), IDocumentItemRepository
    {

    }
}
