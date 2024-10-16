namespace LinkDev.Talabat.APIs.Controllers.Errors
{
    public class ApiValidationErrorResponse : ApiResponse
    {
        public ApiValidationErrorResponse(string? message = null) : base(400)
        {
        }
        public required IEnumerable<ValidationError> Errors { get; set; }


        public class ValidationError
        {
            public required string Field { get; set; }
            public required IEnumerable<string> Errors { get; set; }
        }

    }
}
