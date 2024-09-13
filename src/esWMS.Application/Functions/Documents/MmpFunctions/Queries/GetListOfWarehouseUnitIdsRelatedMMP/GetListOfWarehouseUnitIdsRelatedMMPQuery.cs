using esWMS.Application.Responses;
using MediatR;

namespace esWMS.Application.Functions.Documents.MmpFunctions.Queries.GetListOfWarehouseUnitsInMMP
{
    public record GetListOfWarehouseUnitIdsRelatedMMPQuery(string DocumentId)
        : IRequest<BaseResponse<string[]>>;
}
