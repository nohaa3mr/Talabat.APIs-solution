
namespace Talabat.Apis.ErrorsHandler
{
    public class ErrorApiResponse
    {
        public int StatusCode { get; set; }
        public string? Message { get; set; }

        public ErrorApiResponse(int code , string? message = null)
        {
            code = StatusCode;
            Message = message ?? GetDefaultValue(StatusCode);
        }

        private string? GetDefaultValue(int? code)
        {
            return StatusCode switch
            {
                200 => "Ok",
                204 => "No Content",
                400 => "BadRequest",
                401 => "Unauthorized",
                404 =>"Resources Not Found",
                500 => "Internal Server Error",
                _ => null 

            };
        }
    }
}
