using AutoMapper;
using esWMS.Application.Contracts.Persistence;
using esWMS.Application.Responses;
using MediatR;

namespace esWMS.Application.Functions.Contractors.Queries.GetContractorById
{
    internal class GetContractorByIdQueryHandler
        (IContractorRepository repository,
        IMapper mapper)
        : IRequestHandler<GetContractorByIdQuery, BaseResponse<ContractorDto>>
    {
        private readonly IContractorRepository _repository = repository;
        private readonly IMapper _mapper = mapper;

        public async Task<BaseResponse<ContractorDto>> Handle
            (GetContractorByIdQuery request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.ContractorId))
                return new BaseResponse<ContractorDto>
                    (BaseResponse.ResponseStatus.BadQuery, "No contractor unit IDs provided.");

            var result = await _repository.GetByIdAsync(request.ContractorId);

            var mapped = _mapper.Map<ContractorDto>(result);

            return new BaseResponse<ContractorDto>(mapped);
        }
    }
}
