using esWMS.Application.Responses;
using esWMS.Domain.Models;
using MediatR;
using Sieve.Models;

namespace esWMS.Application.Functions.Employees.Queries.GetSortedFilteredEmployees
{
    public record GetSortedFilteredEmployeessQuery(SieveModel SieveModel)
        : IRequest<BaseResponse<PagedResult<EmployeeDto>>>;
}
