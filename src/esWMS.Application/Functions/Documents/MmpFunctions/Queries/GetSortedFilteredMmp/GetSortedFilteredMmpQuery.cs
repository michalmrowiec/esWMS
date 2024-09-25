using esWMS.Domain.Models;
using esWMS.Application.Responses;
using MediatR;
using Sieve.Models;

namespace esWMS.Application.Functions.Documents.MmpFunctions.Queries.GetSortedFilteredMmp
{
    public record GetSortedFilteredMmpQuery(SieveModel SieveModel)
        : IRequest<BaseResponse<PagedResult<MmpDto>>>;
}
