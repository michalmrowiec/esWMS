using AutoMapper;
using esWMS.Domain.Entities.SystemActors;
using esWMS.Application.Contracts.Persistence;
using esWMS.Application.Responses;
using MediatR;

namespace esWMS.Application.Functions.Contractors.Commands.CreateContractor
{
    public class CreateContractorCommandHandler
        (IContractorRepository repository,
        IMapper mapper)
        : IRequestHandler<CreateContractorCommand, BaseResponse<ContractorDto>>
    {
        private readonly IContractorRepository _repository = repository;
        private readonly IMapper _mapper = mapper;

        public async Task<BaseResponse<ContractorDto>> Handle
            (CreateContractorCommand request, CancellationToken cancellationToken)
        {
            var validationResult =
                await new CreateContractorValidator().ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                return new BaseResponse<ContractorDto>(validationResult);
            }

            var entity = _mapper.Map<Contractor>(request);

            var createdEntity = await _repository.CreateAsync(entity);

            var entityDto = _mapper.Map<ContractorDto>(createdEntity);

            return new BaseResponse<ContractorDto>(entityDto);
        }
    }
}
