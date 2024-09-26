using esWMS.Application.Contracts.Persistence;
using esWMS.Domain.Entities.SystemActors;
using Microsoft.Extensions.Logging;

namespace esWMS.Infrastructure.Repositories.SystemActors
{
    internal class EmployeeRepository
        (EsWmsDbContext context,
        ILogger<EmployeeRepository> logger)
        : IEmployeeRepository
    {

        private readonly EsWmsDbContext _context = context;
        private readonly ILogger<EmployeeRepository> _logger = logger;

        public async Task<Employee> CreateEmployeeAsync(Employee employee)
        {
            try
            {
                await _context.Employees.AddAsync(employee);
                await _context.SaveChangesAsync();
                return employee;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating entity");
                throw;
            }
        }
    }
}
