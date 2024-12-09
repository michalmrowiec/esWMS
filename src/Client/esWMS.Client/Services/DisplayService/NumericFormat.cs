namespace esWMS.Client.Services.DisplayService
{
    public static class NumericFormat
    {
        public static string FormatQuantity(this decimal quantity)
        {
            return quantity.ToString("F2");
        }

        public static string FormatPrice(this decimal quantity)
        {
            return quantity.ToString("F2");
        }

        public static string FormatPrice(this decimal? quantity)
        {
            return quantity?.ToString("F2") ?? "";
        }
    }
}
