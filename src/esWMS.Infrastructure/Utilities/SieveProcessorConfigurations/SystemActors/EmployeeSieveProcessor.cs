using esWMS.Domain.Entities.SystemActors;
using Sieve.Services;

namespace esWMS.Infrastructure.Utilities.SieveProcessorConfigurations.SystemActors
{
    internal class EmployeeSieveProcessor : ISieveConfiguration
    {
        public void Configure(SievePropertyMapper mapper)
        {
            mapper.Property<Employee>(x => x.EmployeeId)
                .CanSort()
                .CanFilter();
            mapper.Property<Employee>(x => x.RoleId)
                .CanSort()
                .CanFilter();
            mapper.Property<Employee>(x => x.FirstName)
                .CanSort()
                .CanFilter();
            mapper.Property<Employee>(x => x.LastName)
                .CanSort()
                .CanFilter();
            mapper.Property<Employee>(x => x.EmailAddress)
                .CanSort()
                .CanFilter();
            mapper.Property<Employee>(x => x.PhoneNumber)
                .CanSort()
                .CanFilter();
            mapper.Property<Employee>(x => x.City)
                .CanSort()
                .CanFilter();
            mapper.Property<Employee>(x => x.Address)
                .CanSort()
                .CanFilter();
            mapper.Property<Contractor>(x => x.PostalCode)
                .CanSort()
                .CanFilter();
            mapper.Property<Employee>(x => x.Region)
                .CanSort()
                .CanFilter();
            mapper.Property<Employee>(x => x.IsActive)
                .CanSort()
                .CanFilter();
        }
    }
}
