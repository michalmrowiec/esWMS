using FluentValidation;

namespace esWMS.Application.Functions.Warehouses.Commands.CreateWarehouse
{
    internal class CreateWarehouseValidator : AbstractValidator<CreateWarehouseCommand>
    {
        public CreateWarehouseValidator()
        {
            RuleFor(x => x.WarehouseId)
                .NotNull()
                .NotEmpty()
                .MaximumLength(3);
        }
    }
}
