using AutoMapper;
using esMWS.Domain.Models;
using esWMS.Application.Contracts.Persistence.Documents;
using esWMS.Application.Responses;
using MediatR;

namespace esWMS.Application.Functions.Documents.PwFunctions.Queries.GetSortedFilteredPw;

internal class GetSortedFilteredPwQueryHandler
    (IPwRepository pwRepository, IMapper mapper)
    : IRequestHandler<GetSortedFilteredPwQuery, BaseResponse<PagedResult<PwDto>>>
{
    private readonly IPwRepository _pwRepository = pwRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<BaseResponse<PagedResult<PwDto>>> Handle
        (GetSortedFilteredPwQuery request, CancellationToken cancellationToken)
    {
        var pagedResult = await _pwRepository.GetSortedFilteredAsync(request.SieveModel);

        var mapped = _mapper.Map<PagedResult<PwDto>>(pagedResult);

        return new BaseResponse<PagedResult<PwDto>>(mapped);
    }
}