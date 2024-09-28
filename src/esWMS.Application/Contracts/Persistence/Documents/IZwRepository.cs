using esWMS.Domain.Entities.Documents;
using esWMS.Domain.Models;
using Sieve.Models;

namespace esWMS.Application.Contracts.Persistence.Documents
{
    public interface IZwRepository
        : IBaseDocumentRepository<ZW>
    {
        Task<PagedResult<DocumentItem>> GetEligibleItemsForZwReturn(SieveModel sieveModel);
    }
}
