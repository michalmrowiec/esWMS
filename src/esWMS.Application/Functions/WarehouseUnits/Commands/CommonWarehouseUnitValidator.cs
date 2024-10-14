using FluentValidation;

namespace esWMS.Application.Functions.WarehouseUnits.Commands
{
    internal class CommonWarehouseUnitValidator<T> : AbstractValidator<T>
        where T : CommonWarehouseUnitCommand
    {
        public CommonWarehouseUnitValidator()
        {

        }
    }
}
