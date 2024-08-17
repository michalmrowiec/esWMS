using AutoMapper;
using esMWS.Domain.Models;
using esWMS.Application.Contracts.Persistence;
using esWMS.Application.Responses;
using MediatR;

namespace esWMS.Application.Functions.Contractors.Queries.GetSortedFilteredContractors
{
    internal class GetSortedFilteredontractorQueryHandler
        (IContractorRepository repository, IMapper mapper)
        : IRequestHandler<GetSortedFilteredContractorQuery, BaseResponse<PagedResult<ContractorDto>>>
    {
        private readonly IContractorRepository _repository = repository;
        private readonly IMapper _mapper = mapper;

        public async Task<BaseResponse<PagedResult<ContractorDto>>> Handle(GetSortedFilteredContractorQuery request, CancellationToken cancellationToken)
        {
            var pagedResult = await _repository.GetSortedFilteredAsync(request.SieveModel);

            var mapped = _mapper.Map<PagedResult<ContractorDto>>(pagedResult);

            return new BaseResponse<PagedResult<ContractorDto>>(mapped);
        }
    }
}