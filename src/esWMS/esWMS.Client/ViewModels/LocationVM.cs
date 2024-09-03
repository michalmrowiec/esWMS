using FluentValidation;

namespace esWMS.Client.ViewModels
{
    public class LocationVM
    {
        public string LocationId { get; set; } = null!;
        public string ZoneId { get; set; } = null!;
        public int Row { get; set; }
        public char Column { get; set; }
        public int Level { get; set; }
        public int Cell { get; set; }
        public int Capacity { get; set; }
        public int MaxLength { get; set; }
        public int MaxWidth { get; set; }
        public int MaxHeight { get; set; }
        public int MaxWeight { get; set; }
        public bool IsBusy { get; set; }
        public string? DefaultMediaTypeId { get; set; }

        public ZoneVM? Zone { get; set; }
    }

    public class CreateLocationVM
    {
        public string ZoneId { get; set; }
        public int Row { get; set; }
        public char Column { get; set; }
        public int Level { get; set; }
        public int Cell { get; set; }
        public int Capacity { get; set; } = 1;
        public int MaxLength { get; set; }
        public int MaxWidth { get; set; }
        public int MaxHeight { get; set; }
        public int MaxWeight { get; set; }
        public bool IsBusy { get; set; } = false;
        public string? DefaultMediaTypeId { get; set; }
        public string? CreatedBy { get; set; }
    }

    public class CreateLocationVMValidator : AbstractValidator<LocationVM>
    {
        public CreateLocationVMValidator()
        {

        }

        public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
        {
            var result = await ValidateAsync(ValidationContext<LocationVM>.CreateWithOptions((LocationVM)model, x => x.IncludeProperties(propertyName)));
            if (result.IsValid)
                return Array.Empty<string>();
            return result.Errors.Select(e => e.ErrorMessage);
        };
    }
}
