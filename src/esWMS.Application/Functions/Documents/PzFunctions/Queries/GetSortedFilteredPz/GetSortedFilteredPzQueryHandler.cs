using AutoMapper;
using esMWS.Domain.Models;
using esWMS.Application.Contracts.Persistence.Documents;
using esWMS.Application.Functions.Documents.PzFunctions;
using esWMS.Application.Functions.Warehouses.Queries.GetAllWarehouses;
using esWMS.Application.Responses;
using MediatR;

namespace esWMS.Application.Functions.Warehouses.Queries.GetSortedFilteredWarehouses
{
    internal class GetSortedFilteredPzQueryHandler
        (IPzRepository pzRepository, IMapper mapper)
        : IRequestHandler<GetSortedFilteredPzQuery, BaseResponse<PagedResult<PzDto>>>
    {
        private readonly IPzRepository _pzRepository = pzRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<BaseResponse<PagedResult<PzDto>>> Handle(GetSortedFilteredPzQuery request, CancellationToken cancellationToken)
        {
            var pagedResult = await _pzRepository.GetSortedFilteredAsync(request.SieveModel);

            var mapped = _mapper.Map<PagedResult<PzDto>>(pagedResult);

            return new BaseResponse<PagedResult<PzDto>>(mapped);
        }
    }
}