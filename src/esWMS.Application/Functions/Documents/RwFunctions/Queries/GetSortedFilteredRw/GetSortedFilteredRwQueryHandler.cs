using AutoMapper;
using esMWS.Domain.Entities.Documents;
using esMWS.Domain.Models;
using esWMS.Application.Contracts.Persistence.Documents;
using esWMS.Application.Functions.Services;
using esWMS.Application.Responses;
using MediatR;

namespace esWMS.Application.Functions.Documents.RwFunctions.Queries.GetSortedFilteredRw
{
    internal class GetSortedFilteredRwQueryHandler
        (IRwRepository rwRepository, IMapper mapper)
        : IRequestHandler<GetSortedFilteredRwQuery, BaseResponse<PagedResult<RwDto>>>
    {
        private readonly IRwRepository _rwRepository = rwRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<BaseResponse<PagedResult<RwDto>>> Handle
            (GetSortedFilteredRwQuery request, CancellationToken cancellationToken)
        {
            return await request.SieveModel.Handle<RwDto, RW>(_mapper, _rwRepository);

        }
    }
}