using esWMS.Application.Responses;
using MediatR;

namespace esWMS.Application.Functions.Documents.BaseDocumentFunctions.Queries.GetDocumentById
{
    public record class GetDocumentByIdQuery(string DocumentId)
        : IRequest<BaseResponse<BaseDocumentDto>>;
}
