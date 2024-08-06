using esWMS.Application.Functions.Documents.PzFunctions;
using esWMS.Application.Responses;
using MediatR;

namespace esWMS.Application.Functions.Documents.BaseDocumentFunctions.Queries.GetDocumentById
{
    public record class GetPzByIdQuery(string PzId)
        : IRequest<BaseResponse<PzDto>>;
}
