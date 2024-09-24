using AutoMapper;
using esMWS.Domain.Models;
using esWMS.Application.Contracts.Persistence;
using esWMS.Application.Responses;
using Sieve.Models;

namespace esWMS.Application.Services
{
    internal static class GetSortedFiltered
    {
        public static async Task<BaseResponse<PagedResult<TDto>>> Handle<TDto, TEntity>
            (this SieveModel sieveModel, IMapper mapper, ISieve<TEntity> repository)
            where TEntity : class
            where TDto : class
        {
            var pagedResult = await repository.GetSortedFilteredAsync(sieveModel);

            var mapped = mapper.Map<PagedResult<TDto>>(pagedResult);

            return new BaseResponse<PagedResult<TDto>>(mapped);
        }
    }
}
