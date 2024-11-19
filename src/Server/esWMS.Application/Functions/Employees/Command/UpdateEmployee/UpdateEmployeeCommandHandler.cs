using AutoMapper;
using esWMS.Application.Contracts.Persistence;
using esWMS.Application.Responses;
using MediatR;

namespace esWMS.Application.Functions.Employees.Command.UpdateEmployee
{
    internal class UpdateEmployeeCommandHandler(
        IEmployeeRepository repository,
        IMapper mapper) :
        IRequestHandler<UpdateEmployeeCommand, BaseResponse<EmployeeDto>>
    {
        private readonly IEmployeeRepository _repository = repository;
        private readonly IMapper _mapper = mapper;

        public async Task<BaseResponse<EmployeeDto>> Handle
            (UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await new UpdateEmployeeValidator().
                ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                return new BaseResponse<EmployeeDto>(validationResult);
            }

            try
            {
                var existingEntity = await _repository.GetByIdAsync(request.EmployeeId);

                var mapped = _mapper.Map(request, existingEntity);

                var updated = await _repository.UpdateAsync(mapped);

                var mappedDto = _mapper.Map<EmployeeDto>(updated);

                return new BaseResponse<EmployeeDto>(mappedDto);
            }
            catch (KeyNotFoundException ex)
            {
                return new BaseResponse<EmployeeDto>
                    (BaseResponse.ResponseStatus.NotFound, "Employee not found.");
            }
            catch (Exception ex)
            {
                return new BaseResponse<EmployeeDto>
                    (BaseResponse.ResponseStatus.ServerError, "Something went wrong.");
            }
        }
    }
}
