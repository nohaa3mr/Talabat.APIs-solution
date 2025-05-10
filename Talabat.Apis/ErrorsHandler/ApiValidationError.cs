namespace Talabat.Apis.ErrorsHandler
{
    public class ApiValidationError : ErrorApiResponse
    {
        public IEnumerable<string> Errors { get; set; }
        public ApiValidationError():base(400)
        {
            Errors = new List<string>();
            
        }
    }
}
