using FluentValidation;

namespace esWMS.Client.ViewModels.Documents
{
    public class WzVM : BaseDocumentVM
    {
        public DateTime? GoodsReleaseDate { get; set; }
        public string RecipientContractorId { get; set; } = null!;

        public ContractorVM? RecipientContractor { get; set; }
    }

    public class CreateWzVM
    {
        public string? IssueWarehouseId { get; set; }
        public string? Comment { get; set; }
        public DateTime? DocumentIssueDate { get; set; } = DateTime.Now;
        public string? AssignedEmployeeId { get; set; }
        public DateTime? GoodsReleaseDate { get; set; } = DateTime.Now;
        public string? RecipientContractorId { get; set; }
        public IList<DocumentItemVM> DocumentItems { get; set; } = [];
    }

    public class CreateWzVMValidator : AbstractValidator<CreateWzVM>
    {
        public CreateWzVMValidator()
        {

        }

        public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
        {
            var result = await ValidateAsync(ValidationContext<CreateWzVM>.CreateWithOptions((CreateWzVM)model, x => x.IncludeProperties(propertyName)));
            if (result.IsValid)
                return Array.Empty<string>();
            return result.Errors.Select(e => e.ErrorMessage);
        };
    }
}
