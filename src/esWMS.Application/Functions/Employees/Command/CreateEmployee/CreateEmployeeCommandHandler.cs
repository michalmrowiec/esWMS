using esWMS.Application.Responses;
using MediatR;

namespace esWMS.Application.Functions.Employees.Command.CreateEmployee
{
    internal class CreateEmployeeCommandHandler
        : IRequestHandler<CreateEmployeeCommand, BaseResponse<EmployeeDto>>
    {
        public Task<BaseResponse<EmployeeDto>> Handle
            (CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
