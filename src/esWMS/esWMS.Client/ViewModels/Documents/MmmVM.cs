using FluentValidation;

namespace esWMS.Client.ViewModels.Documents
{
    public class MmmDetailsVM : MmmVM
    {
        public List<string> RelatedWarehouseUnitIds { get; set; } = [];
    }

    public class MmmVM : BaseDocumentVM
    {
        public DateTime? GoodsReleaseDate { get; set; }
        public string ToWarehouseId { get; set; } = null!;

        public WarehouseVM? ToWarehouse { get; set; }
        public MmpVM? RelatedMmp { get; set; }
    }

    public class CreateMmmVM
    {
        public string? IssueWarehouseId { get; set; }
        public string? Comment { get; set; }
        public DateTime? DocumentIssueDate { get; set; } = DateTime.Now;
        public string? IssuingEmployeeId { get; set; }
        public string? AssignedEmployeeId { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? GoodsReleaseDate { get; set; } = DateTime.Now;
        public string? ToWarehouseId { get; set; }

        public List<string> WarehouseUnitIds { get; set; } = [];
    }

    public class CreateMmmVMValidator : AbstractValidator<CreateMmmVM>
    {
        public CreateMmmVMValidator()
        {

        }

        public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
        {
            var result = await ValidateAsync(ValidationContext<CreateMmmVM>.CreateWithOptions((CreateMmmVM)model, x => x.IncludeProperties(propertyName)));
            if (result.IsValid)
                return Array.Empty<string>();
            return result.Errors.Select(e => e.ErrorMessage);
        };
    }
}
