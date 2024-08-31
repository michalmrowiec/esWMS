using AutoMapper;
using esMWS.Domain.Models;
using esWMS.Application.Contracts.Persistence.Documents;
using esWMS.Application.Functions.Documents.DocumentItemsFunctions;
using esWMS.Application.Responses;
using MediatR;

namespace esWMS.Application.Functions.Documents.ZwFunctions.Queries.GetEligibleItemsForZwReturn
{
    internal class GetEligibleItemsForZwReturnQueryHandler
        (IZwRepository repository, IMapper mapper)
        : IRequestHandler<GetEligibleItemsForZwReturnQuery, BaseResponse<PagedResult<DocumentItemDto>>>
    {
        private readonly IZwRepository _repository = repository;
        private readonly IMapper _mapper = mapper;

        public async Task<BaseResponse<PagedResult<DocumentItemDto>>> Handle
            (GetEligibleItemsForZwReturnQuery request, CancellationToken cancellationToken)
        {
            var pagedResult = await _repository.GetEligibleItemsForZwReturn(request.SieveModel);

            var mapped = _mapper.Map<PagedResult<DocumentItemDto>>(pagedResult);

            return new BaseResponse<PagedResult<DocumentItemDto>>(mapped);
        }
    }
}
