using esWMS.Application.Functions.Employees;
using esWMS.Application.Responses;
using MediatR;

namespace esWMS.Application.Functions.Employees.Queries.GetEmployeeById
{
    public record GetEmployeeByIdQuery(string EmployeeId)
        : IRequest<BaseResponse<EmployeeDto>>;
}
