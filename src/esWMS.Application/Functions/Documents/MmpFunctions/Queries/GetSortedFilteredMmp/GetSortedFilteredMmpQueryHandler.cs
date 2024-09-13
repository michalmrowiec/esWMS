using AutoMapper;
using esMWS.Domain.Entities.Documents;
using esMWS.Domain.Models;
using esWMS.Application.Contracts.Persistence.Documents;
using esWMS.Application.Functions.Services;
using esWMS.Application.Responses;
using MediatR;

namespace esWMS.Application.Functions.Documents.MmpFunctions.Queries.GetSortedFilteredMmp
{
    internal class GetSortedFilteredMmpQueryHandler
        (IMmpRepository mmpRepository, IMapper mapper)
        : IRequestHandler<GetSortedFilteredMmpQuery, BaseResponse<PagedResult<MmpDto>>>
    {
        private readonly IMmpRepository _mmpRepository = mmpRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<BaseResponse<PagedResult<MmpDto>>> Handle(GetSortedFilteredMmpQuery request, CancellationToken cancellationToken)
        {
            return await request.SieveModel.Handle<MmpDto, MMP>(_mapper, _mmpRepository);
        }
    }
}