using System.Security;

namespace esWMS.Client.Services.DisplayService
{
    public static class NumericFormat
    {
        public const string QuantityStringFormat = "F2";
        public const string PriceStringFormat = "F2";
        public static string FormatQuantity(this decimal quantity)
        {
            return quantity.ToString(QuantityStringFormat);
        }

        public static string FormatPrice(this decimal quantity)
        {
            return quantity.ToString(PriceStringFormat);
        }

        public static string FormatPrice(this decimal? quantity)
        {
            return quantity?.ToString(PriceStringFormat) ?? string.Empty;
        }
    }
}
