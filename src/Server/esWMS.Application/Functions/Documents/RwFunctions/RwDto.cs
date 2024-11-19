using esWMS.Application.Functions.Documents.BaseDocumentFunctions;

namespace esWMS.Application.Functions.Documents.RwFunctions
{
    public class RwDto : BaseDocumentDto
    {
        public DateTime? GoodsReleaseDate { get; set; }
        public string? DepartmentName { get; set; }
    }
}
