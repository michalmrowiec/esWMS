﻿namespace esWMS.Client.ViewModels.WarehouseEnvironment.Location
{
    public class UpdateLocationVM
    {
        public string LocationId { get; set; } = null!;
        public decimal Capacity { get; set; }
        public double? MaxLength { get; set; }
        public double? MaxWidth { get; set; }
        public double? MaxHeight { get; set; }
        public double? MaxWeight { get; set; }
        public string? DefaultMediaTypeId { get; set; }
    }
}