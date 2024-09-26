using esWMS.Domain.Models;
using FluentValidation;

namespace esWMS.Application.Functions.Employees.Command.CreateEmployee
{
    internal class CreateEmployeeValidator : AbstractValidator<CreateEmployeeCommand>
    {
        public CreateEmployeeValidator()
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

            RuleFor(r => r.Password)
                .NotEmpty()
                .MinimumLength(4)
                .MaximumLength(60);

            RuleFor(r => r.RepeatPassword)
                .Equal(r => r.Password)
                .WithMessage("Passwords are not the same");
        }
    }
}
