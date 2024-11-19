using esWMS.Application.Functions.Documents.MmpFunctions;
using esWMS.Application.Responses;
using MediatR;

namespace esWMS.Application.Functions.Documents.WzFunctions.Queries.GetWzById
{
    public record class GetMmpByIdQuery(string DocumentId)
        : IRequest<BaseResponse<MmpDto>>;
}
