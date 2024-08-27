using FluentValidation;

namespace esWMS.Client.ViewModels.Documents
{
    public class PwVM : BaseDocumentVM
    {
        public DateTime? GoodsReceiptDate { get; set; }
        public string? DepartmentName { get; set; }
    }
    public class CreatePwVM
    {
        public string? IssueWarehouseId { get; set; }
        public string? Comment { get; set; }
        public DateTime? DocumentIssueDate { get; set; } = DateTime.Now;
        public string? AssignedEmployeeId { get; set; }
        public DateTime? GoodsReceiptDate { get; set; } = DateTime.Now;
        public string? DepartmentName { get; set; }
        public IList<DocumentItemVM> DocumentItems { get; set; } = [];
    }
    public class CreatePwVMValidator : AbstractValidator<CreatePwVM>
    {
        public CreatePwVMValidator()
        {

        }

        public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
        {
            var result = await ValidateAsync(ValidationContext<CreatePwVM>.CreateWithOptions((CreatePwVM)model, x => x.IncludeProperties(propertyName)));
            if (result.IsValid)
                return Array.Empty<string>();
            return result.Errors.Select(e => e.ErrorMessage);
        };
    }
}
