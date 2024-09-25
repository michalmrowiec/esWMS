using NanoidDotNet;

namespace esWMS.Domain.Services
{
    public static class WarehouseUnitIdGenerator
    {
        static string customAlphabet = "ABCDEFGHJKLMNPQRSTUVWXYZabcdefghijkmnopqrstuvwxyz0123456789"; // Without O, I, l

        public static string WarehouseUnitId()
        {
            return Nanoid.Generate(alphabet: customAlphabet, size: 8);
        }

        public static string WarehouseUnitItemId()
        {
            return Nanoid.Generate(alphabet: customAlphabet, size: 10);
        }
    }
}
