using esMWS.Domain.Models;
using esWMS.Application.Responses;
using MediatR;
using Sieve.Models;

namespace esWMS.Application.Functions.Documents.ZwFunctions.Queries.GetSortedFilteredZw
{
    public record GetSortedFilteredZwQuery(SieveModel SieveModel)
        : IRequest<BaseResponse<PagedResult<ZwDto>>>;
}
