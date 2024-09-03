using AutoMapper;
using esMWS.Domain.Entities.SystemActors;
using esMWS.Domain.Models;
using esWMS.Application.Contracts.Persistence;
using esWMS.Application.Functions.Services;
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

        public async Task<BaseResponse<PagedResult<ContractorDto>>> Handle
            (GetSortedFilteredContractorQuery request, CancellationToken cancellationToken)
        {
            return await request.SieveModel.Handle<ContractorDto, Contractor>(_mapper, _repository);
        }
    }
}