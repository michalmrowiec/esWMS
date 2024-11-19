using FluentValidation;

namespace esWMS.Client.ViewModels.Documents
{
    public class RwVM : BaseDocumentVM
    {
        public DateTime? GoodsReleaseDate { get; set; }
        public string? DepartmentName { get; set; }
    }

    public class CreateRwVM
    {
        public string? IssueWarehouseId { get; set; }
        public string? Comment { get; set; }
        public DateTime? DocumentIssueDate { get; set; } = DateTime.Now;
        public string? AssignedEmployeeId { get; set; }
        public DateTime? GoodsReleaseDate { get; set; } = DateTime.Now;
        public string? DepartmentName { get; set; }
        public IList<DocumentItemVM> DocumentItems { get; set; } = [];
    }

    public class CreateRwVMValidator : AbstractValidator<CreateRwVM>
    {
        public CreateRwVMValidator()
        {

        }

        public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
        {
            var result = await ValidateAsync(ValidationContext<CreateRwVM>.CreateWithOptions((CreateRwVM)model, x => x.IncludeProperties(propertyName)));
            if (result.IsValid)
                return Array.Empty<string>();
            return result.Errors.Select(e => e.ErrorMessage);
        };
    }
}
