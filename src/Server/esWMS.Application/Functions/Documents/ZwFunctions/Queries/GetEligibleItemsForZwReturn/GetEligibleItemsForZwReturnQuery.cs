using esWMS.Domain.Models;
using esWMS.Application.Functions.Documents.DocumentItemsFunctions;
using esWMS.Application.Responses;
using MediatR;
using Sieve.Models;

namespace esWMS.Application.Functions.Documents.ZwFunctions.Queries.GetEligibleItemsForZwReturn
{
    public record GetEligibleItemsForZwReturnQuery(SieveModel SieveModel)
        : IRequest<BaseResponse<PagedResult<DocumentItemDto>>>;
}
