using esWMS.Application.Responses;
using MediatR;

namespace esWMS.Application.Functions.Employees.Command.CreateEmployee
{
    public class CreateEmployeeCommand :
        CommonEmployeeCommand,
        IRequest<BaseResponse<EmployeeDto>>
    {
        public string Password { get; set; } = null!;
        public string RepeatPassword { get; set; } = null!;
        public string? CreatedBy { get; set; }
    }
}
