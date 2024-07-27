namespace esWMS.Application.Responses
{
    public class BaseResponse
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public List<string>? ValidationErrors { get; set; }

        public BaseResponse()
        {
            Success = true;
            ValidationErrors = new();
        }

        public BaseResponse(bool status, string message)
        {
            Success = status;
            Message = message;
            ValidationErrors = new();
        }

        public BaseResponse(FluentValidation.Results.ValidationResult validationResult)
        {
            Success = false;
            ValidationErrors = new();
            validationResult.Errors
                .ForEach(e => ValidationErrors.Add(e.ErrorMessage));
        }

        public BaseResponse(bool success, string? message, FluentValidation.Results.ValidationResult validationResult)
        {
            Success = success;
            Message = message;
            ValidationErrors = new();
            validationResult.Errors
                .ForEach(e => ValidationErrors.Add(e.ErrorMessage));
        }
    }

    public class BaseResponse<T> : BaseResponse where T : class
    {
        public T? ReturnedObj { get; set; }

        public BaseResponse(T obj)
        {
            Success = true;
            ValidationErrors = [];
            ReturnedObj = obj;
        }

        public BaseResponse(FluentValidation.Results.ValidationResult validationResult) : base(validationResult) { }

        public BaseResponse(bool status, string message) : base(status, message) { }
    }
}
