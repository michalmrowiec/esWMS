using esWMS.Application.Responses;
using MediatR;

namespace esWMS.Application.Functions.Documents.WzFunctions.Queries.GetWzById
{
    public record class GetWzByIdQuery(string WzId)
        : IRequest<BaseResponse<WzDto>>;
}
