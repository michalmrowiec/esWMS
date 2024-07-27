namespace esMWS.Domain.Entities.WarehouseEnviroment
{
    public class Location
    {
        public string LocationId { get; set; } = null!;
        public string ZoneId { get; set; } = null!;
        public int Row { get; set; }
        public char Column { get; set; }
        public int Level { get; set; }
        public int Cell { get; set; }
        public int Capacity { get; set; }
        public int MaxLength { get; set; }
        public int MaxWidth { get; set; }
        public int MaxHeight { get; set; }
        public int MaxWeight { get; set; }
        public bool IsBusy { get; set; }
        public string? DefaultMediaTypeId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public string? ModifiedBy { get; set; }

        public Zone? Zone { get; set; }
        public Product? DefaultMediaType { get; set; }
        public IList<WarehouseUnit> WarehouseUnits { get; set; } = [];
    }
}
