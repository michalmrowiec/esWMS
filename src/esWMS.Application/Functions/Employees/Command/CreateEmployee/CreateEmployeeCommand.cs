using esWMS.Application.Responses;
using MediatR;

namespace esWMS.Application.Functions.Employees.Command.CreateEmployee
{
    public class CreateEmployeeCommand : IRequest<BaseResponse<EmployeeDto>>
    {
        public string EmployeeId { get; set; } = null!;
        public string RoleId { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string RepeatPassword { get; set; } = null!;
        public string? EmailAddress { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Country { get; set; }
        public string? City { get; set; }
        public string? Region { get; set; }
        public string? PostalCode { get; set; }
        public string? Address { get; set; }
        public bool IsActive { get; set; }
        public string? CreatedBy { get; set; }
    }
}
