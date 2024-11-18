namespace esWMS.Application.Functions.Employees.Command
{
    public abstract class CommonEmployeeCommand
    {
        public string EmployeeId { get; set; } = null!;
        public string RoleId { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? EmailAddress { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Country { get; set; }
        public string? City { get; set; }
        public string? Region { get; set; }
        public string? PostalCode { get; set; }
        public string? Address { get; set; }
        public bool IsActive { get; set; }
    }
}
