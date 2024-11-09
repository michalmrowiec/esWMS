using esWMS.Application.Responses;
using MediatR;

namespace esWMS.Application.Functions.Documents.MmpFunctions.Queries.GetMmpDetailsById
{
    public record class GetMmpDetailsByIdQuery(string DocumentId)
        : IRequest<BaseResponse<MmpDetailsDto>>;
}
