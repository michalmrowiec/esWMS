using AutoMapper;
using esWMS.Application.Contracts.Persistence;
using esWMS.Application.Functions.Employees;
using esWMS.Application.Functions.Employees.Queries.GetEmployeeById;
using esWMS.Application.Responses;
using MediatR;

namespace esWMS.Application.Functions.Contractors.Queries.GetContractorById
{
    internal class GetEmployeeByIdQueryHandler(
        IEmployeeRepository repository,
        IMapper mapper)
        : IRequestHandler<GetEmployeeByIdQuery, BaseResponse<EmployeeDto>>
    {
        private readonly IEmployeeRepository _repository = repository;
        private readonly IMapper _mapper = mapper;

        public async Task<BaseResponse<EmployeeDto>> Handle(
            GetEmployeeByIdQuery request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.EmployeeId))
                return new BaseResponse<EmployeeDto>
                    (BaseResponse.ResponseStatus.BadQuery, "No employee ID provided.");

            var result = await _repository.GetByIdAsync(request.EmployeeId);

            var mapped = _mapper.Map<EmployeeDto>(result);

            return new BaseResponse<EmployeeDto>(mapped);
        }
    }
}
