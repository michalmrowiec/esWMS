using esWMS.Application.Functions.Documents.MmmFunctions;
using esWMS.Application.Responses;
using MediatR;

namespace esWMS.Application.Functions.Documents.WzFunctions.Queries.GetWzById
{
    public record class GetMmmByIdQuery(string DocumentId)
        : IRequest<BaseResponse<MmmDto>>;
}
