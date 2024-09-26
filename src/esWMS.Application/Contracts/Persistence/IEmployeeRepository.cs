using esWMS.Domain.Entities.SystemActors;

namespace esWMS.Application.Contracts.Persistence
{
    public interface IEmployeeRepository
        : ISieve<Employee>
    {
        Task<Employee> CreateEmployeeAsync(Employee employee);
    }
}
