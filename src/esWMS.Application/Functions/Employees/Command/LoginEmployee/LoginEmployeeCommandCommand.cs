using AutoMapper;
using esWMS.Application.Contracts.Persistence;
using esWMS.Application.Contracts.Services;
using esWMS.Application.Responses;
using esWMS.Domain.Entities.SystemActors;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace esWMS.Application.Functions.Employees.Command.LoginEmployee
{
    internal class LoginEmployeeCommandCommand
        (IEmployeeRepository employeeRepository,
        IMapper mapper,
        IPasswordHasher<Employee> passwordHasher,
        IJwtTokenService jwtTokenService)
        : IRequestHandler<LoginEmployeeCommand, BaseResponse<LogedEmployeeDto>>
    {
        private readonly IEmployeeRepository _employeeRepository = employeeRepository;
        private readonly IMapper _mapper = mapper;
        private readonly IPasswordHasher<Employee> _passwordHasher = passwordHasher;
        private readonly IJwtTokenService _jwtTokenService = jwtTokenService;

        public async Task<BaseResponse<LogedEmployeeDto>> Handle(LoginEmployeeCommand request, CancellationToken cancellationToken)
        {
            Employee employee;
            try
            {
                employee = await _employeeRepository.GetByIdAsync(request.EmployeeId);
            }
            catch (KeyNotFoundException ex)
            {
                return new BaseResponse<LogedEmployeeDto>
                    (BaseResponse.ResponseStatus.BadQuery, "Login or password are wrong.");
            }
            catch (Exception ex)
            {
                return new BaseResponse<LogedEmployeeDto>
                    (BaseResponse.ResponseStatus.ServerError, "Something went wrong.");
            }

            var veryfication = _passwordHasher.VerifyHashedPassword(employee, employee.PasswordHash, request.Password);

            if (veryfication == PasswordVerificationResult.Failed)
            {
                return new BaseResponse<LogedEmployeeDto>
                    (BaseResponse.ResponseStatus.BadQuery, "Login or password are wrong.");
            }

            string jwtToken = _jwtTokenService.GenerateJwt(employee);

            LogedEmployeeDto logedEmployee = new(employee.EmployeeId, employee.RoleId, jwtToken);

            return new BaseResponse<LogedEmployeeDto>(logedEmployee);
        }
    }
}
