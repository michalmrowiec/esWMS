using esMWS.Domain.Entities.Documents;
using esWMS.Application.Contracts.Persistence.Documents;

namespace esWMS.Application.Contracts.Persistence
{
    public interface IWzRepozitory
        : IBaseDocumentRepository<WZ>
    { }
}
