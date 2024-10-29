using FluentValidation;

namespace esWMS.Client.ViewModels.WarehouseEnviroment.Warehouse
{
    public class CreateWarehouseVMValidator : AbstractValidator<CreateWarehouseVM>
    {
        public CreateWarehouseVMValidator()
        {

        }

        public Func<object, string, Task<IEnumerable<string>>> ValidateValue =>
            async (model, propertyName) =>
        {
            var result = await ValidateAsync(
                ValidationContext<CreateWarehouseVM>.CreateWithOptions(
                    (CreateWarehouseVM)model, x => x.IncludeProperties(propertyName)));
            if (result.IsValid)
                return Array.Empty<string>();
            return result.Errors.Select(e => e.ErrorMessage);
        };
    }
}
