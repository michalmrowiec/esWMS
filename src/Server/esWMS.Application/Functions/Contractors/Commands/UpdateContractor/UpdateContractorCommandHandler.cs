using AutoMapper;
using esWMS.Application.Contracts.Persistence;
using esWMS.Application.Functions.Products.Commands.CreateProduct;
using esWMS.Application.Functions.Products;
using esWMS.Application.Responses;
using MediatR;

namespace esWMS.Application.Functions.Contractors.Commands.UpdateContractor
{
    internal class UpdateContractorCommandHandler
        (IContractorRepository repository,
        IMapper mapper)
        : IRequestHandler<UpdateContractorCommand, BaseResponse<ContractorDto>>
    {
        private readonly IContractorRepository _repository = repository;
        private readonly IMapper _mapper = mapper;

        public async Task<BaseResponse<ContractorDto>> Handle
            (UpdateContractorCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await new UpdateContractorValidator().ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                return new BaseResponse<ContractorDto>(validationResult);
            }

            try
            {
                var existingEntity = await _repository.GetByIdAsync(request.ContractorId);

                var mapped = _mapper.Map(request, existingEntity);

                var updated = await _repository.UpdateAsync(mapped);

                var mappedDto = _mapper.Map<ContractorDto>(updated);

                return new BaseResponse<ContractorDto>(mappedDto);
            }
            catch (KeyNotFoundException ex)
            {
                return new BaseResponse<ContractorDto>
                    (BaseResponse.ResponseStatus.NotFound, "Contractor not found.");
            }
            catch (Exception ex)
            {
                return new BaseResponse<ContractorDto>
                    (BaseResponse.ResponseStatus.ServerError, "Something went wrong.");
            }
        }
    }
}
