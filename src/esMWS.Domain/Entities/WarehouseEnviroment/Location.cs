using System.ComponentModel.DataAnnotations;

namespace esMWS.Domain.Entities.WarehouseEnviroment
{
    public class Location
    {
        [Required]
        [MaxLength(11)]
        public string LocationId { get; set; } = null!;
        [Required]
        public string ZoneId { get; set; } = null!;
        [Required]
        public int Row { get; set; }
        [Required]
        public char Column { get; set; }
        [Required]
        public int Level { get; set; }
        [Required]
        public int Cell { get; set; }
        [Required]
        public int Capacity { get; set; }
        public int MaxLength { get; set; }
        public int MaxWidth { get; set; }
        public int MaxHeight { get; set; }
        public int MaxWeight { get; set; }
        [Required]
        public bool IsBusy { get; set; }
        public string? DefaultMediaTypeId { get; set; }

        public Zone? Zone { get; set; }
        public Product? DefaultMediaType { get; set; }
    }
}
