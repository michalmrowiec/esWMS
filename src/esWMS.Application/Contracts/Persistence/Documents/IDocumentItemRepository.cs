using esWMS.Domain.Entities.Documents;
using System.Security.Cryptography;

namespace esWMS.Application.Contracts.Persistence.Documents
{
    public interface IDocumentItemRepository
        : IBaseRepository<DocumentItem>
    {
        Task<DocumentItem> GetDocumentItemByIdWithAssignments(string documnetItemId);

        Task DeleteRangeAsync(params string[] documentItemIds);
    }
}
