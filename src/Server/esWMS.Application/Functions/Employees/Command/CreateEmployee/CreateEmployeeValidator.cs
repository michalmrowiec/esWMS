using FluentValidation;

namespace esWMS.Application.Functions.Employees.Command.CreateEmployee
{
    internal class CreateEmployeeValidator : CommonEmployeeValidator<CreateEmployeeCommand>
    {
        public CreateEmployeeValidator()
        {
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
