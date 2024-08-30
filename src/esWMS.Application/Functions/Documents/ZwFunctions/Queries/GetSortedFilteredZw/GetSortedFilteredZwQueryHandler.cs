using AutoMapper;
using esMWS.Domain.Models;
using esWMS.Application.Contracts.Persistence.Documents;
using esWMS.Application.Responses;
using MediatR;

namespace esWMS.Application.Functions.Documents.ZwFunctions.Queries.GetSortedFilteredZw;

internal class GetSortedFilteredZwQueryHandler
    (IZwRepository repository, IMapper mapper)
    : IRequestHandler<GetSortedFilteredZwQuery, BaseResponse<PagedResult<ZwDto>>>
{
    private readonly IZwRepository _repository = repository;
    private readonly IMapper _mapper = mapper;

    public async Task<BaseResponse<PagedResult<ZwDto>>> Handle
        (GetSortedFilteredZwQuery request, CancellationToken cancellationToken)
    {
        var pagedResult = await _repository.GetSortedFilteredAsync(request.SieveModel);

        var mapped = _mapper.Map<PagedResult<ZwDto>>(pagedResult);

        return new BaseResponse<PagedResult<ZwDto>>(mapped);
    }
}