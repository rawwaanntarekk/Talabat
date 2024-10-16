using LinkDev.Talabat.APIs.Controllers.Errors;
using LinkDev.Talabat.APIs.Controllers.Exceptions;
using System.Net;
using System.Text.Json;

namespace LinkDev.Talabat.APIs.Middlewares
{
    // Convension-Based
    public class CustomExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<CustomExceptionHandlerMiddleware> _logger;
        private readonly IWebHostEnvironment _env;

        public CustomExceptionHandlerMiddleware(RequestDelegate next, ILogger<CustomExceptionHandlerMiddleware> logger , IWebHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                // Logic Executed With the Request
                await _next(httpContext);

                // Logic Executed With the Response
            }
            catch (Exception ex)
            {
                        ApiResponse response;
                switch (ex)
                {
                    case NotFoundException:

                        httpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
                        httpContext.Response.ContentType = "application/json";
                        response = new ApiResponse((int)HttpStatusCode.NotFound, ex.Message);
                        await httpContext.Response.WriteAsync(response.ToString());
                        break;

                    default:

                        if (_env.IsDevelopment())
                        {
                            // Developement Mode
                            _logger.LogError(ex, ex.Message);
                            response = new ApiExceptionResponse((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace!.ToString());

                        }
                        else
                        {

                            // Production Mode
                            // Log Exception Details in Database | File (Text, Json)

                            response = new ApiExceptionResponse((int)HttpStatusCode.InternalServerError);

                        }

                        httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        httpContext.Response.ContentType = "application/json";

                        await httpContext.Response.WriteAsync(response.ToString());
                    break;

                }
               
                
            }
        }
    }
}
