using esMWS.Domain.Entities.Documents;
using esMWS.Domain.Models;
using Sieve.Models;

namespace esWMS.Application.Contracts.Persistence.Documents
{
    public interface IZwRepository
        : IBaseDocumentRepository<ZW>
    {
        Task<PagedResult<DocumentItem>> GetEligibleItemsForZwReturn(SieveModel sieveModel);
    }
}
