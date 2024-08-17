using esMWS.Domain.Models;
using esWMS.Application.Responses;
using MediatR;
using Sieve.Models;

namespace esWMS.Application.Functions.Contractors.Queries.GetSortedFilteredContractors
{
    public record GetSortedFilteredContractorQuery(SieveModel SieveModel)
        : IRequest<BaseResponse<PagedResult<ContractorDto>>>;
}
