using esMWS.Domain.Entities.Documents;
using esWMS.Application.Responses;
using MediatR;

namespace esWMS.Application.Functions.Documents.BaseDocumentFunctions.Queries.GetDocumentById
{
    public record class GetDocumentByIdQuery<TDocument, TDocumentDto>(string DocumentId)
        : IRequest<BaseResponse<TDocumentDto>>
        where TDocument : BaseDocument
        where TDocumentDto : BaseDocumentDto;
}
