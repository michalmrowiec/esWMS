namespace esWMS.Application.Functions.Employees
{
    public class LogedEmployeeDto
    {
        public string EmployeeId { get; set; } = null!;
        public string RoleId { get; set; } = null!;
        public string JwtToken { get; set; } = null!;

        public LogedEmployeeDto(string employeeId, string roleId, string jwtToken)
        {
            EmployeeId = employeeId;
            RoleId = roleId;
            JwtToken = jwtToken;
        }
    }
}
