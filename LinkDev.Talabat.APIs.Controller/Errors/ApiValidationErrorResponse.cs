namespace LinkDev.Talabat.APIs.Controllers.Errors
{
    public class ApiValidationErrorResponse : ApiResponse
    {
        public ApiValidationErrorResponse(string? message = null) : base(400)
        {
        }
        public required IEnumerable<string> Errors { get; set; }


        
    }
}
