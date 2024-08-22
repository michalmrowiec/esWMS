using esMWS.Domain.Models;
using esWMS.Application.Responses;
using MediatR;
using Sieve.Models;

namespace esWMS.Application.Functions.Documents.MmmFunctions.Queries.GetSortedFilteredMmm
{
    public record GetSortedFilteredMmmQuery(SieveModel SieveModel)
        : IRequest<BaseResponse<PagedResult<MmmDto>>>;
}
