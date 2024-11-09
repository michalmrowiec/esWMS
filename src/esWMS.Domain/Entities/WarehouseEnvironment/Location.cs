using System.ComponentModel.DataAnnotations.Schema;

namespace esWMS.Domain.Entities.WarehouseEnviroment
{
    public class Location
    {
        public string LocationId { get; set; } = null!;
        public string ZoneId { get; set; } = null!;
        public int Row { get; set; }
        public char Column { get; set; }
        public int Level { get; set; }
        public int Cell { get; set; }
        public decimal Capacity { get; set; }
        public decimal? MaxLength { get; set; }
        public decimal? MaxWidth { get; set; }
        public decimal? MaxHeight { get; set; }
        public decimal? MaxWeight { get; set; }
        public string? DefaultMediaTypeId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public string? ModifiedBy { get; set; }

        public Zone? Zone { get; set; }
        public Product? DefaultMediaType { get; set; }
        public IList<WarehouseUnit> WarehouseUnits { get; set; } = [];

        [NotMapped]
        public bool IsFull => Capacity <= WarehouseUnits.Count;
    }
}
