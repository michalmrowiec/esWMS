using AutoMapper;
using esMWS.Domain.Entities.Documents;
using esMWS.Domain.Models;
using esWMS.Application.Contracts.Persistence.Documents;
using esWMS.Application.Responses;
using esWMS.Application.Services;
using MediatR;

namespace esWMS.Application.Functions.Documents.ZwFunctions.Queries.GetSortedFilteredZw;

internal class GetSortedFilteredZwQueryHandler
    (IZwRepository zwRepository, IMapper mapper)
    : IRequestHandler<GetSortedFilteredZwQuery, BaseResponse<PagedResult<ZwDto>>>
{
    private readonly IZwRepository _zwRepository = zwRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<BaseResponse<PagedResult<ZwDto>>> Handle
        (GetSortedFilteredZwQuery request, CancellationToken cancellationToken)
    {
        return await request.SieveModel.Handle<ZwDto, ZW>(_mapper, _zwRepository);

    }
}