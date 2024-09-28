using AutoMapper;
using esWMS.Domain.Entities.WarehouseEnviroment;
using esWMS.Domain.Models;
using esWMS.Application.Contracts.Persistence;
using esWMS.Application.Responses;
using esWMS.Application.Services;
using MediatR;

namespace esWMS.Application.Functions.Categories.Queries.GetSortedFilteredCategories
{
    internal class GetSortedFilteredCategoriesQueryHandler
        (ICategoryRepository repository, IMapper mapper)
        : IRequestHandler<GetSortedFilteredCategoriesQuery, BaseResponse<PagedResult<CategoryDto>>>
    {
        private readonly ICategoryRepository _repository = repository;
        private readonly IMapper _mapper = mapper;

        public async Task<BaseResponse<PagedResult<CategoryDto>>> Handle
            (GetSortedFilteredCategoriesQuery request, CancellationToken cancellationToken)
        {
            return await request.SieveModel.Handle<CategoryDto, Category>(_mapper, _repository);
        }
    }
}