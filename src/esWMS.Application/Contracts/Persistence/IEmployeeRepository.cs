using esWMS.Domain.Entities.SystemActors;

namespace esWMS.Application.Contracts.Persistence
{
    public interface IEmployeeRepository
    {
        Task<Employee> CreateEmployeeAsync(Employee employee);
    }
}
