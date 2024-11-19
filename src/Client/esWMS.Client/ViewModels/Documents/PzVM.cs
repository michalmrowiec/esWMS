using esWMS.Client.ViewModels.SystemActors;
using FluentValidation;

namespace esWMS.Client.ViewModels.Documents
{
    public class PzVM : BaseDocumentVM
    {
        public DateTime? GoodsReceiptDate { get; set; }
        public string SupplierContractorId { get; set; } = null!;

        public ContractorVM? SupplierContractor { get; set; }
    }

    public class CreatePzVM
    {
        public string? IssueWarehouseId { get; set; }
        public string? Comment { get; set; }
        public DateTime? DocumentIssueDate { get; set; } = DateTime.Now;
        public string? AssignedEmployeeId { get; set; }
        public DateTime? GoodsReceiptDate { get; set; } = DateTime.Now;
        public string? SupplierContractorId { get; set; }
        public IList<DocumentItemVM> DocumentItems { get; set; } = [];
    }

    public class CreatePzVMValidator : AbstractValidator<CreatePzVM>
    {
        public CreatePzVMValidator()
        {

        }

        public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
        {
            var result = await ValidateAsync(ValidationContext<CreatePzVM>.CreateWithOptions((CreatePzVM)model, x => x.IncludeProperties(propertyName)));
            if (result.IsValid)
                return Array.Empty<string>();
            return result.Errors.Select(e => e.ErrorMessage);
        };
    }
}
