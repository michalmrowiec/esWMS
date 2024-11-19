using esWMS.Application.Functions.Documents.PwFunctions;
using esWMS.Application.Responses;
using MediatR;

namespace esWMS.Application.Functiosns.Documents.PwFunctions.Queries.GetPwById
{
    public record class GetPwByIdQuery(string PwId)
        : IRequest<BaseResponse<PwDto>>;
}
