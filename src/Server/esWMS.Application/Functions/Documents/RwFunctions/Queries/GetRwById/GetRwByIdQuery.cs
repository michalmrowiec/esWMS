using esWMS.Application.Responses;
using MediatR;

namespace esWMS.Application.Functions.Documents.RwFunctions.Queries.GetRwById
{
    public record class GetRwByIdQuery(string WzId)
        : IRequest<BaseResponse<RwDto>>;
}
