using FluentValidation;

namespace esWMS.Client.ViewModels
{
    public class WarehouseUnitVM
    {
        public string WarehouseUnitId { get; set; } = null!;
        public string WarehouseId { get; set; } = null!;
        public string? LocationId { get; set; }
        public decimal? TotalWeight { get; set; }
        public decimal? TotalLength { get; set; }
        public decimal? TotalWidth { get; set; }
        public decimal? TotalHeight { get; set; }
        public bool IsBlocked { get; set; }
        public bool? CanBeStacked { get; set; }
        public string? StackOnId { get; set; }

        public IList<WarehouseUnitItemVM> WarehouseUnitItems { get; set; } = [];
    }

    public class CreateWarehouseUnitVM
    {
        public string? WarehouseId { get; set; }
        public string? LocationId { get; set; }
        public decimal? TotalWeight { get; set; }
        public decimal? TotalLength { get; set; }
        public decimal? TotalWidth { get; set; }
        public decimal? TotalHeight { get; set; }
        public bool? CanBeStacked { get; set; }
        public string? StackOnId { get; set; }
    }

    public class CreateWarehouseUnitVMValidator : AbstractValidator<CreateWarehouseUnitVM>
    {
        public CreateWarehouseUnitVMValidator()
        {

        }

        public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
        {
            var result = await ValidateAsync(ValidationContext<CreateWarehouseUnitVM>.CreateWithOptions((CreateWarehouseUnitVM)model, x => x.IncludeProperties(propertyName)));
            if (result.IsValid)
                return Array.Empty<string>();
            return result.Errors.Select(e => e.ErrorMessage);
        };
    }
}
