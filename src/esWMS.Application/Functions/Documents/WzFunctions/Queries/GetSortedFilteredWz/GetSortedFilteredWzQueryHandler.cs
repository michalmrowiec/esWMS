using AutoMapper;
using esMWS.Domain.Entities.Documents;
using esMWS.Domain.Models;
using esWMS.Application.Contracts.Persistence;
using esWMS.Application.Responses;
using esWMS.Application.Services;
using MediatR;

namespace esWMS.Application.Functions.Documents.WzFunctions.Queries.GetSortedFilteredWz
{
    internal class GetSortedFilteredWzQueryHandler
        (IWzRepository wzRepository, IMapper mapper)
        : IRequestHandler<GetSortedFilteredWzQuery, BaseResponse<PagedResult<WzDto>>>
    {
        private readonly IWzRepository _wzRepository = wzRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<BaseResponse<PagedResult<WzDto>>> Handle
            (GetSortedFilteredWzQuery request, CancellationToken cancellationToken)
        {
            return await request.SieveModel.Handle<WzDto, WZ>(_mapper, _wzRepository);

        }
    }
}