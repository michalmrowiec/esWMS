using esWMS.Domain.Models;
using FluentValidation;

namespace esWMS.Application.Functions.Employees.Command
{
    internal abstract class CommonEmployeeValidator<T> : AbstractValidator<T>
        where T : CommonEmployeeCommand
    {
        public CommonEmployeeValidator()
        {
            RuleFor(x => x.EmployeeId)
                .NotEmpty()
                .MaximumLength(60);

            RuleFor(r => r.RoleId)
                .NotEmpty()
                .Custom((value, context) =>
                {
                    if (!Roles.IsValidRole(value))
                    {
                        context.AddFailure(
                            "RoleId",
                            $"Invalid role. The employee role can have the following values: {string.Join(',', Roles.RoleDescriptions.Select(x => $"{x.Key} ({x.Value})"))}");
                    }
                });
        }
    }
}
