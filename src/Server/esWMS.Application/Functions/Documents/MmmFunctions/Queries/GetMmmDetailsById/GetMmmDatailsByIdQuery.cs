using esWMS.Application.Responses;
using MediatR;

namespace esWMS.Application.Functions.Documents.MmmFunctions.Queries.GetMmmDetailsById
{
    public record class GetMmmDetailsByIdQuery(string DocumentId)
        : IRequest<BaseResponse<MmmDetailsDto>>;
}
