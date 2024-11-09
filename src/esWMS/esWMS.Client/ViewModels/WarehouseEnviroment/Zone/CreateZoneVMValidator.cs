using FluentValidation;

namespace esWMS.Client.ViewModels
{
    public class CreateZoneVMValidator : AbstractValidator<CreateZoneVM>
    {
        public CreateZoneVMValidator()
        {

        }


        public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
        {
            var result = await ValidateAsync(ValidationContext<CreateZoneVM>.CreateWithOptions((CreateZoneVM)model, x => x.IncludeProperties(propertyName)));
            if (result.IsValid)
                return Array.Empty<string>();
            return result.Errors.Select(e => e.ErrorMessage);
        };
    }
}
