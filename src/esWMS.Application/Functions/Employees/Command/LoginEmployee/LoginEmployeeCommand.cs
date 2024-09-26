using esWMS.Application.Responses;
using MediatR;

namespace esWMS.Application.Functions.Employees.Command.LoginEmployee
{
    public class LoginEmployeeCommand : IRequest<BaseResponse<LogedEmployeeDto>>
    {
        public string EmployeeId { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
