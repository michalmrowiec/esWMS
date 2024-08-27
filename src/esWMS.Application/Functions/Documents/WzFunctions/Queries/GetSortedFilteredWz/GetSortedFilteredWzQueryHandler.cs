using AutoMapper;
using esMWS.Domain.Models;
using esWMS.Application.Contracts.Persistence;
using esWMS.Application.Responses;
using MediatR;

namespace esWMS.Application.Functions.Documents.WzFunctions.Queries.GetSortedFilteredWz
{
    internal class GetSortedFilteredWzQueryHandler
        (IWzRepository WzRepository, IMapper mapper)
        : IRequestHandler<GetSortedFilteredWzQuery, BaseResponse<PagedResult<WzDto>>>
    {
        private readonly IWzRepository _WzRepository = WzRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<BaseResponse<PagedResult<WzDto>>> Handle(GetSortedFilteredWzQuery request, CancellationToken cancellationToken)
        {
            var pagedResult = await _WzRepository.GetSortedFilteredAsync(request.SieveModel);

            var mapped = _mapper.Map<PagedResult<WzDto>>(pagedResult);

            return new BaseResponse<PagedResult<WzDto>>(mapped);
        }
    }
}