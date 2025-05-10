using System.Text.Json;
using Talabat.Apis.ErrorsHandler;

namespace Talabat.Apis.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _env;

        public ExceptionMiddleware(RequestDelegate Next , ILogger<ExceptionMiddleware> Logger , IHostEnvironment  env)
        {
            _next = Next;
            _logger = Logger;
            _env = env;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);       //to invoke the next middleware 
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,ex.Message);
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = 500;
                var Response = _env.IsDevelopment() ? new ApiExceptionErrorResponse(500, ex.Message, ex.StackTrace.ToString()) : new ApiExceptionErrorResponse(500);
                var Options = new JsonSerializerOptions()
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };
                var jsonresponse = JsonSerializer.Serialize(Response , Options);

               await context.Response.WriteAsync(jsonresponse);

            }
        }
    }
}
