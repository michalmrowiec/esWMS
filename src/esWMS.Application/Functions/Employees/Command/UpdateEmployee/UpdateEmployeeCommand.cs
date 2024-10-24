using esWMS.Application.Responses;
using MediatR;

namespace esWMS.Application.Functions.Employees.Command.UpdateEmployee
{
    public class UpdateEmployeeCommand :
        CommonEmployeeCommand,
        IRequest<BaseResponse<EmployeeDto>>
    {
        public string? ModifiedBy { get; set; }
    }
}
