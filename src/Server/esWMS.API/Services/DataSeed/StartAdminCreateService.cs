using esWMS.Application.Functions.Employees.Command.CreateEmployee;
using esWMS.Domain.Models;
using esWMS.Infrastructure;
using MediatR;

namespace esWMS.API.Services.DataSeed
{
    internal static class StartAdminCreateService
    {
        public static async Task SeedStartAdmin(this EsWmsDbContext dbContext, IMediator mediator)
        {
            if (!dbContext.Employees.Where(x => x.RoleId == Roles.Admin).Any())
            {
                await mediator.Send(StartAdmin);
            }
        }

        public static CreateEmployeeCommand StartAdmin = new()
        {
            EmployeeId = "admin",
            RoleId = Roles.Admin,
            FirstName = "admin",
            LastName = "admin",
            Password = "admin",
            RepeatPassword = "admin",
            IsActive = true
        };
    }
}