using esWMS.Domain.Entities.Documents;
using esWMS.Application.Contracts.Persistence.Documents;

namespace esWMS.Application.Contracts.Persistence
{
    public interface IWzRepository
        : IBaseDocumentRepository<WZ>, ISieve<WZ>
    { }
}
