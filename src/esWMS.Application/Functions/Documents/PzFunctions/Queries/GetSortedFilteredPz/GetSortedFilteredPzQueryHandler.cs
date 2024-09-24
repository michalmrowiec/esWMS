using AutoMapper;
using esMWS.Domain.Entities.Documents;
using esMWS.Domain.Models;
using esWMS.Application.Contracts.Persistence.Documents;
using esWMS.Application.Functions.Documents.PzFunctions;
using esWMS.Application.Functions.Warehouses.Queries.GetAllWarehouses;
using esWMS.Application.Responses;
using esWMS.Application.Services;
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
            return await request.SieveModel.Handle<PzDto, PZ>(_mapper, _pzRepository);

        }
    }
}