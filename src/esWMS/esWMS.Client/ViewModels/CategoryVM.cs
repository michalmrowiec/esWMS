using FluentValidation;

namespace esWMS.Client.ViewModels
{
    public class CategoryVM
    {
        public string CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string? ParentCategoryId { get; set; }

        public CategoryVM? ParentCategory { get; set; }
    }

    public class CreateCategoryVMValidator : AbstractValidator<CategoryVM>
    {
        public CreateCategoryVMValidator()
        {
            RuleFor(x => x.CategoryName)
                .NotNull()
                .NotEmpty()
                .MinimumLength(1)
                .MaximumLength(50);
        }

        public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
        {
            var result = await ValidateAsync(ValidationContext<CategoryVM>.CreateWithOptions((CategoryVM)model, x => x.IncludeProperties(propertyName)));
            if (result.IsValid)
                return Array.Empty<string>();
            return result.Errors.Select(e => e.ErrorMessage);
        };
    }
}