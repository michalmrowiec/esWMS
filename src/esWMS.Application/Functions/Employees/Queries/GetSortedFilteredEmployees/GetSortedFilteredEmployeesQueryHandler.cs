using AutoMapper;
using esWMS.Application.Contracts.Persistence;
using esWMS.Application.Functions.Employees;
using esWMS.Application.Functions.Employees.Queries.GetSortedFilteredEmployees;
using esWMS.Application.Responses;
using esWMS.Application.Services;
using esWMS.Domain.Entities.SystemActors;
using esWMS.Domain.Models;
using MediatR;

namespace esWMS.Application.Functions.Products.Queries.GetSortedFilteredProducts
{
    internal class GetSortedFilteredEmployeessQueryHandler
        (IEmployeeRepository employeeRepository,
        IMapper mapper)
        : IRequestHandler<GetSortedFilteredEmployeessQuery, BaseResponse<PagedResult<EmployeeDto>>>
    {
        private readonly IEmployeeRepository _employeeRepository = employeeRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<BaseResponse<PagedResult<EmployeeDto>>> Handle
            (GetSortedFilteredEmployeessQuery request, CancellationToken cancellationToken)
        {
            return await request.SieveModel.Handle<EmployeeDto, Employee>(_mapper, _employeeRepository);
        }
    }
}