﻿namespace esWMS.Application.Functions.Zones
{
    public class ZoneDto
    {
        public string ZoneId { get; set; } = null!;
        public string? ZoneName { get; set; }
        public char ZoneAlias { get; set; }
        public string WarehouseId { get; set; } = null!;
        public int? AvgTemperature { get; set; }
    }
}