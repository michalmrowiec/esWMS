using esWMS.Application.Responses;
using MediatR;

namespace esWMS.Application.Functions.Documents.ZwFunctions.Queries.GetZwById
{
    public record class GetZwByIdQuery(string PwId)
        : IRequest<BaseResponse<ZwDto>>;
}
