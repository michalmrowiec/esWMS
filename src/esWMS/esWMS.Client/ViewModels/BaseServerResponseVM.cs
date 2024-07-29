namespace esWMS.Client.ViewModels
{
    public class BaseServerResponseVM<T>
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public List<string>? ValidationErrors { get; set; }
        public T? ReturnedObj { get; set; }
    }
}
