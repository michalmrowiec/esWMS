using AutoMapper;
using esMWS.Domain.Models;
using esWMS.Application.Contracts.Persistence.Documents;
using esWMS.Application.Responses;
using MediatR;

namespace esWMS.Application.Functions.Documents.MmmFunctions.Queries.GetSortedFilteredMmm
{
    internal class GetSortedFilteredMmmQueryHandler
        (IMmmRepository mmmRepository, IMapper mapper)
        : IRequestHandler<GetSortedFilteredMmmQuery, BaseResponse<PagedResult<MmmDto>>>
    {
        private readonly IMmmRepository _mmmRepository = mmmRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<BaseResponse<PagedResult<MmmDto>>> Handle(GetSortedFilteredMmmQuery request, CancellationToken cancellationToken)
        {
            var pagedResult = await _mmmRepository.GetSortedFilteredAsync(request.SieveModel);

            var mapped = _mapper.Map<PagedResult<MmmDto>>(pagedResult);

            return new BaseResponse<PagedResult<MmmDto>>(mapped);
        }
    }
}