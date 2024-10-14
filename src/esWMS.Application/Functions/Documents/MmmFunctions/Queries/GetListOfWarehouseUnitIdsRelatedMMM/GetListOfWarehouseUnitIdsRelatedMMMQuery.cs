using esWMS.Application.Responses;
using MediatR;

namespace esWMS.Application.Functions.Documents.MmpFunctions.Queries.GetListOfWarehouseUnitsInMMM
{
    public record GetListOfWarehouseUnitIdsRelatedMMMQuery(string DocumentId)
        : IRequest<BaseResponse<string[]>>;
}
