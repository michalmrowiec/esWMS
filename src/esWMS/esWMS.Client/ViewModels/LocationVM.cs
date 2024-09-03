namespace esWMS.Client.ViewModels
{
    public class LocationVM
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

        public ZoneVM? Zone { get; set; }
    }
}
