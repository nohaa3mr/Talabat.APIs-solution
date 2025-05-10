namespace Talabat.Apis.ErrorsHandler
{
    public class ApiExceptionErrorResponse :ErrorApiResponse
    {
        public string? Details { get; set; }
        public ApiExceptionErrorResponse(int statuscode , string?message = null , string?details = null) :base(500)
        {
            Details = details;
        }
    }
}
