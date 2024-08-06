namespace esWMS.Client.Components.TableColumnsFilters
{
    public class FilterOption
    {
        public FilterOption(string column, string @operator = "@=")
        {
            Column = column;
            Operator = @operator;
            Value = "";
        }

        public FilterOption(string column, string @operator, string value)
        {
            Column = column;
            Operator = @operator;
            Value = value;
        }

        public string Column { get; set; }
        public string Operator { get; set; }
        public string Value { get; set; }
    }
}
