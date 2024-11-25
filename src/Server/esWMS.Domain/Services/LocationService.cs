using esWMS.Domain.Entities.WarehouseEnvironment;

namespace esWMS.Domain.Services
{
    public static class LocationService
    {
        public static string GenerateLocationId(this Location location) =>
            $"{location.ZoneId}/{location.Row:D2}/{location.Column}/{location.Level}/{location.Cell}".ToUpper();
    }
}
