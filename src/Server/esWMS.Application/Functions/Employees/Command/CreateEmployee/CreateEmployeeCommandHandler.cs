using AutoMapper;
using esWMS.Application.Contracts.Persistence;
using esWMS.Application.Responses;
using esWMS.Domain.Entities.SystemActors;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace esWMS.Application.Functions.Employees.Command.CreateEmployee
{
    internal class CreateEmployeeCommandHandler
        (IEmployeeRepository employeeRepository,
        IMapper mapper,
        IPasswordHasher<Employee> passwordHasher)
        : IRequestHandler<CreateEmployeeCommand, BaseResponse<EmployeeDto>>
    {
        private readonly IEmployeeRepository _employeeRepository = employeeRepository;
        private readonly IMapper _mapper = mapper;
        private readonly IPasswordHasher<Employee> _passwordHasher = passwordHasher;

        public async Task<BaseResponse<EmployeeDto>> Handle
            (CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var validationResult =
            await new CreateEmployeeValidator().ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                return new BaseResponse<EmployeeDto>(validationResult);
            }

            Employee newEmployee = _mapper.Map<Employee>(request);
            newEmployee.PasswordHash = _passwordHasher.HashPassword(newEmployee, request.Password);
            newEmployee.CreatedAt = DateTime.UtcNow;

            EmployeeDto employeeDto;
            try
            {
                var createdEmployee = await _employeeRepository.CreateEmployeeAsync(newEmployee);

                employeeDto = _mapper.Map<EmployeeDto>(createdEmployee);
            }
            catch (Exception ex)
            {
                return new BaseResponse<EmployeeDto>(BaseResponse.ResponseStatus.ServerError, "Something went wrong.");
            }

            return new BaseResponse<EmployeeDto>(employeeDto);
        }
    }
}
