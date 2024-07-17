using System.ComponentModel.DataAnnotations;

namespace esMWS.Domain.Entities.WarehouseEnviroment
{
    public class Category
    {
        [Required]
        [StringLength(450)]
        public string CategoryId { get; set; } = null!;
        [Required]
        [StringLength(50)]
        public string CategoryName { get; set; } = null!;
        public string? ParentCategoryId { get; set; }
    }
}
