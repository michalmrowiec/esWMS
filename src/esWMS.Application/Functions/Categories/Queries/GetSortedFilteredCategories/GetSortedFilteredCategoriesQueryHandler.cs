using AutoMapper;
using esMWS.Domain.Models;
using esWMS.Application.Contracts.Persistence;
using esWMS.Application.Responses;
using MediatR;

namespace esWMS.Application.Functions.Categories.Queries.GetSortedFilteredCategories
{
    internal class GetSortedFilteredCategoriesQueryHandler
        (ICategoryRepository repository, IMapper mapper)
        : IRequestHandler<GetSortedFilteredCategoriesQuery, BaseResponse<PagedResult<CategoryDto>>>
    {
        private readonly ICategoryRepository _repository = repository;
        private readonly IMapper _mapper = mapper;

        public async Task<BaseResponse<PagedResult<CategoryDto>>> Handle(GetSortedFilteredCategoriesQuery request, CancellationToken cancellationToken)
        {
            var pagedResult = await _repository.GetSortedFilteredProductsAsync(request.SieveModel);

            var mapped = _mapper.Map<PagedResult<CategoryDto>>(pagedResult);

            return new BaseResponse<PagedResult<CategoryDto>>(mapped);
        }
    }
}