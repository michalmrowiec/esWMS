using FluentValidation;

namespace esWMS.Client.ViewModels.Documents
{
    public class ZwVM : BaseDocumentVM
    {
        public DateTime? GoodsReceiptDate { get; set; }
        public string? DepartmentName { get; set; }
    }
    public class CreateZwVM
    {
        public string? IssueWarehouseId { get; set; }
        public string? Comment { get; set; }
        public DateTime? DocumentIssueDate { get; set; } = DateTime.Now;
        public string? AssignedEmployeeId { get; set; }
        public DateTime? GoodsReceiptDate { get; set; } = DateTime.Now;
        public string? DepartmentName { get; set; }
        public IList<DocumentItemVM> DocumentItems { get; set; } = [];
        public IList<DocumentItemIdQuantityVM> DocumentItemIdQuantity { get; set; } = [];

        public class DocumentItemIdQuantityVM
        {
            public string DocumentItemId { get; set; } = null!;
            public int Quantity { get; set; }
        }
    }
    public class CreateZwVMValidator : AbstractValidator<CreateZwVM>
    {
        public CreateZwVMValidator()
        {

        }

        public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
        {
            var result = await ValidateAsync(ValidationContext<CreateZwVM>.CreateWithOptions((CreateZwVM)model, x => x.IncludeProperties(propertyName)));
            if (result.IsValid)
                return Array.Empty<string>();
            return result.Errors.Select(e => e.ErrorMessage);
        };
    }
}
