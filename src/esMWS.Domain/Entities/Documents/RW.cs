using System.ComponentModel.DataAnnotations;

namespace esMWS.Domain.Entities.Documents
{
    public class RW : DocumentBase
    {
        public DateTime? GoodsReleaseDate { get; set; }
        [MaxLength(100)]
        public string? DepartmentName { get; set; }
    }
}
