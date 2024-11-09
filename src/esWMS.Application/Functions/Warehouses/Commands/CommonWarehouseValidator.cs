using FluentValidation;

namespace esWMS.Application.Functions.Warehouses.Commands
{
    internal abstract class CommonWarehouseValidator<T> : AbstractValidator<T>
        where T : CommonWarehouseCommand
    {
        public CommonWarehouseValidator()
        {
            RuleFor(x => x.WarehouseId)
                .NotNull()
                .NotEmpty()
                .MaximumLength(3);
        }
    }
}
