using esMWS.Domain.Models;
using esWMS.Application.Responses;
using MediatR;
using Sieve.Models;

namespace esWMS.Application.Functions.Documents.RwFunctions.Queries.GetSortedFilteredRw
{
    public record GetSortedFilteredRwQuery(SieveModel SieveModel)
        : IRequest<BaseResponse<PagedResult<RwDto>>>;
}
