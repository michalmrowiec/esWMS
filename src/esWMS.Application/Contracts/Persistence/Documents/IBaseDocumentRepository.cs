namespace esWMS.Application.Contracts.Persistence.Documents
{
    public interface IBaseDocumentRepository<TDocument>
        : IBaseRepository<TDocument>
        where TDocument : class
    {
        Task<TDocument> GetDocumentByIdWithItems(string id);
    }
}
