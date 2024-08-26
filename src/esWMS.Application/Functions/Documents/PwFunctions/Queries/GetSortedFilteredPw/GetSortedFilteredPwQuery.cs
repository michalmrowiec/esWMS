using esMWS.Domain.Models;
using esWMS.Application.Responses;
using MediatR;
using Sieve.Models;

namespace esWMS.Application.Functions.Documents.PwFunctions.Queries.GetSortedFilteredPw
{
    public record GetSortedFilteredPwQuery(SieveModel SieveModel)
        : IRequest<BaseResponse<PagedResult<PwDto>>>;
}
