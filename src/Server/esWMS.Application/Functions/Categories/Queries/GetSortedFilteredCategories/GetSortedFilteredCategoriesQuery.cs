using esWMS.Domain.Models;
using esWMS.Application.Responses;
using MediatR;
using Sieve.Models;

namespace esWMS.Application.Functions.Categories.Queries.GetSortedFilteredCategories
{
    public record GetSortedFilteredCategoriesQuery(SieveModel SieveModel)
        : IRequest<BaseResponse<PagedResult<CategoryDto>>>;
}
