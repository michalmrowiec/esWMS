﻿namespace esMWS.Domain.Entities.WarehouseEnviroment
{
    public class Zone
    {
        public string ZoneId { get; set; } = null!;
        public string? ZoneName { get; set; }
        public char ZoneAlias { get; set; }
        public string WarehouseId { get; set; } = null!;
        public int? AvgTemperature { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public string? ModifiedBy { get; set; }

        public Warehouse? Warehouse { get; set; }
        public IList<Location> Locations { get; set; } = [];
    }
}
