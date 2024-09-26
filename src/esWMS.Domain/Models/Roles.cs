namespace esWMS.Domain.Models
{
    public class Roles
    {
        public const string Admin = "ADM";
        public const string Manager = "MNG";
        public const string Operator = "OPE";

        public static readonly Dictionary<string, string> RoleDescriptions = new Dictionary<string, string>
        {
            { "ADM", "Admin" },
            { "MNG", "Manager" },
            { "OPE", "Operator" }
        };

        public static bool IsValidRole(string roleId) => RoleDescriptions.ContainsKey(roleId);
    }
}
