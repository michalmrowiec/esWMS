using System.ComponentModel.DataAnnotations;

namespace esMWS.Domain.Entities.WarehouseEnviroment
{
    public class Zone
    {
        [Required]
        [MaxLength(5)]
        public string ZoneId { get; set; } = null!;
        [MaxLength(30)]
        public string? ZoneName { get; set; }
        [Required]
        public char ZoneAlias { get; set; }
        [Required]
        public string WarehouseId { get; set; } = null!;
        public int? AvgTemperature { get; set; }

        public Warehouse? Warehouse { get; set; }
        public IList<Location>? Locations { get; set; }
    }
}
