using esWMS.Domain.Entities.SystemActors;
using esWMS.Application.Contracts.Persistence;

namespace esWMS.Infrastructure.Repositories.SystemActors
{
    internal class EmployeeRepository : IEmployeeRepository
    {
        public Task<Employee> CreateEmployeeAsync(Employee employee)
        {
            throw new NotImplementedException();
        }
    }
}
