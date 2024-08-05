using esMWS.Domain.Entities.Documents;

namespace esWMS.Application.Contracts.Persistence.Documents
{
    public interface IBaseDocumentRepository<TDocument>
        : IBaseRepository<TDocument>, ISieve<TDocument>
        where TDocument : BaseDocument
    {
        Task<TDocument> GetDocumentByIdWithItemsAsync(string id);
        Task<string[]> GetAllDocumentIdForDay(DateTime date);
    }
}
