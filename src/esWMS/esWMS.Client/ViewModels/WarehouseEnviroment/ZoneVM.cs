using esWMS.Client.ViewModels.WarehouseEnviroment.Location;
using FluentValidation;

namespace esWMS.Client.ViewModels
{
    public class ZoneVM
    {
        public string ZoneId { get; set; } = null!;
        public string? ZoneName { get; set; }
        public char ZoneAlias { get; set; }
        public string WarehouseId { get; set; } = null!;
        public decimal? AvgTemperature { get; set; }

        public WarehouseVM? Warehouse { get; set; }
        public IList<LocationVM> Locations { get; set; } = [];
    }


    public class CreateZoneVM
    {
        public string? ZoneName { get; set; }
        public char? ZoneAlias { get; set; }
        public string? WarehouseId { get; set; }
        public decimal? AvgTemperature { get; set; }
    }

    public class CreateZoneVMValidator : AbstractValidator<CreateZoneVM>
    {
        public CreateZoneVMValidator()
        {

        }


        public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
        {
            var result = await ValidateAsync(ValidationContext<CreateZoneVM>.CreateWithOptions((CreateZoneVM)model, x => x.IncludeProperties(propertyName)));
            if (result.IsValid)
                return Array.Empty<string>();
            return result.Errors.Select(e => e.ErrorMessage);
        };
    }
}
